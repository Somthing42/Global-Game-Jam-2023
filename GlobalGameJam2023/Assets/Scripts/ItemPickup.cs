using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public bool isHealth;

    public bool isAmmo;

    public float healthAmount;

    public int ammoAmount;
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //if pick up is health
            if (isHealth)
            {
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
            }
            if (isAmmo)
            {
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
            }
        }
    }
}
