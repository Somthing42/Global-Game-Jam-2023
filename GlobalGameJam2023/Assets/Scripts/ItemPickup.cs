using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    public bool isHealth;

    public bool isAmmo;

    public float healthAmount;

    public int ammoAmount;

    public AudioClip pickupSound;

    public Text IntIncreaseText;

    Color col;
    private void Start()
    {
        //col = IntIncreaseText.color;
    }
    public bool pickedUp;
    private void Update()
    {
       
    }
    public void OnTriggerEnter(Collider other)
    {
        pickedUp = true;
        if (other.gameObject.tag == "Player")
        {   
            
            GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioSource>().PlayOneShot(pickupSound);
           
           /// col.a = 255;
            
            //if pick up is health
            if (isHealth)
            {
               // IntIncreaseText.text = "+" + healthAmount;
                //if current health is not at max
                if (other.gameObject.GetComponent<Damageable>().currentHealth < other.gameObject.GetComponent<Damageable>().maxHealth)
                {
                    //heal player
                    other.gameObject.GetComponent<Damageable>().currentHealth += healthAmount;
                    
                    //if player is healed to higher then max
                    if(other.gameObject.GetComponent<Damageable>().currentHealth > other.gameObject.GetComponent<Damageable>().maxHealth)
                    {

                        //then set current health to max health
                       other.gameObject.GetComponent<Damageable>().currentHealth = other.gameObject.GetComponent<Damageable>().maxHealth;
                    }
                    //update ui to reflect change
                    GameManager.instance.UpdateHealth(other.gameObject.GetComponent<Damageable>().currentHealth);
                    
                    Destroy(gameObject);
                }
                Destroy(gameObject);
            }
            if (isAmmo)
            {
                //.text = "+" + ammoAmount;
                if (GameManager.instance.gun.GetComponent<Gun>().currentAmmo < GameManager.instance.gun.GetComponent<Gun>().maxAmmo)
                {
                    GameManager.instance.gun.GetComponent<Gun>().currentAmmo += ammoAmount;
                    

                    if (GameManager.instance.gun.GetComponent<Gun>().currentAmmo > GameManager.instance.gun.GetComponent<Gun>().maxAmmo)
                    {
                        GameManager.instance.gun.GetComponent<Gun>().currentAmmo = GameManager.instance.gun.GetComponent<Gun>().maxAmmo;
                    }
                    GameManager.instance.UpdateAmmo(GameManager.instance.gun.GetComponent<Gun>().currentAmmo);

                    Destroy(gameObject);
                }
                Destroy(gameObject);
            }
        }
    }
}
