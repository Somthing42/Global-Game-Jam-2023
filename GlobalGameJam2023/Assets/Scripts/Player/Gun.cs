using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun : MonoBehaviour
{
    Transform cam;
    [SerializeField] GameObject player;

    [Header("General Stats")]
    [SerializeField] float range = 50.0f;
    [SerializeField] float damage = 10.0f;

    public int maxAmmo;
    [HideInInspector] public int currentAmmo;

    //[SerializeField] float fireRate = 5.0f;
    [SerializeField] float reloadTime;
   

    [SerializeField] int bulletsPerShot = 6;

    [SerializeField] float inaccuracyDistance = 5.0f;

    [SerializeField] GameObject muzzleFlash;
    [SerializeField] Transform muzzleLocation;

    [Header("SwaySettings")]
    [SerializeField] float smooth;
    [SerializeField] float swayMultiplayer;

    [Header("Laser")]
    [SerializeField] GameObject laser;
    [SerializeField] float fadeDuration = 0.3f;

    [Header("Gun Sounds")]
    public AudioClip[] gunSounds;

    private void Awake()
    {
        cam = Camera.main.transform;
      
      
        currentAmmo = maxAmmo;
    }

    private void Update()
    {
        WeaponSway();
    }

    public void Shoot()
    {
        if (CanShoot()){
            //for animations
            if (!gameObject.GetComponent<Animator>().GetBool("Fired")) {

                gameObject.GetComponent<Animator>().SetBool("Fired", true);
                PlayGunShot();

                currentAmmo--;
                GameManager.instance.UpdateAmmo(currentAmmo);
                //Instantiate(muzzleFlash, muzzleFlashLocation.position, muzzleFlashLocation.rotation);


                gameObject.GetComponent<Animator>().Play("ShotgunRecoil");
                for (int i = 0; i < bulletsPerShot; i++)
                {
                    RaycastHit hit;
                    Vector3 shootingDir = GettShootingDirection();
                    if (Physics.Raycast(cam.position, shootingDir, out hit, range))
                    {
                        if (hit.collider.GetComponent<Damageable>() != null)
                        {
                            hit.collider.GetComponent<Damageable>().TakeDamage(damage, hit.point, hit.normal);
                        }
                        // CreateLaser(hit.point);
                    }
                    else
                    {
                        // CreateLaser(cam.position + shootingDir * range);
                    }
                }



            }
        }
        else
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(gunSounds[2]);
        }
    }


    bool CanShoot()
    {
        bool enoughAmmo = currentAmmo > 0;
        return enoughAmmo;
    }

    Vector3 GettShootingDirection()
    {
        Vector3 targetPos = cam.position + cam.forward * range;
        targetPos = new Vector3(
            targetPos.x + Random.Range(-inaccuracyDistance, inaccuracyDistance),
            targetPos.y + Random.Range(-inaccuracyDistance, inaccuracyDistance),
            targetPos.z + Random.Range(-inaccuracyDistance, inaccuracyDistance)
            );
        Vector3 direction = targetPos - cam.position;
        return direction.normalized;
    }


    void CreateLaser(Vector3 endPos)
    {
        LineRenderer lr = Instantiate(laser).GetComponent<LineRenderer>();

        lr.SetPositions(new Vector3[2] {muzzleLocation.position, endPos});
        StartCoroutine(FadeLaser(lr));
    }

    IEnumerator FadeLaser(LineRenderer lr)
    {
        float alpha = 1;
        if (lr) 
        { 

         while (alpha > 0)
            {
                alpha -= Time.deltaTime / fadeDuration;
                lr.startColor = new Color(lr.startColor.r, lr.startColor.g, lr.startColor.b, alpha);
                lr.endColor = new Color(lr.endColor.r, lr.endColor.g, lr.endColor.b, alpha);
                yield return null;
            }
         }
    }

    public void WeaponSway()
    {
        float mouseX = (player.GetComponent<PlayerMouseLook>().mouseX * swayMultiplayer) + 90.0f;
        float mouseY = (player.GetComponent<PlayerMouseLook>().mouseY * swayMultiplayer);

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation,targetRotation, smooth * Time.deltaTime);
    }

    public void PlayGunShot()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(gunSounds[0]);
    }

    public void PlayGunPump()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(gunSounds[1]);
    }
}
