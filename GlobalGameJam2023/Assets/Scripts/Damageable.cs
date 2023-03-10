using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public float maxHealth = 100.0f;
    public float currentHealth;

    public bool isDead;

    [SerializeField] GameObject hitEffect;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage, Vector3 hitPos, Vector3 hitNormal)
    {

        Instantiate(hitEffect, hitPos, Quaternion.LookRotation(hitNormal));
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Die();
        }

        if (gameObject.tag == "Player")
        {
            GameManager.instance.UpdateHealth(currentHealth);
        }

    }

    void Die()
    {

        if(gameObject.tag == "Player" && !isDead)
        {
            GameManager.instance.GameOver();
        }
        else
        {
            // print(name + " was destroyed");
            Destroy(gameObject);
        }
       
    }
}
