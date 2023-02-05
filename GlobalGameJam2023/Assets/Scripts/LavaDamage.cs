using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDamage : MonoBehaviour
{
    public float damage;
    public float damRate;
    public float cdamRate;

    private void Start()
    {
        cdamRate = damRate;
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
             
            cdamRate -= Time.deltaTime;

            if (cdamRate <= 0)
            {
                other.gameObject.GetComponent<Damageable>().TakeDamage(damage, other.gameObject.transform.position, other.gameObject.transform.position);
                cdamRate = damRate;
            }
        }
    }
}
