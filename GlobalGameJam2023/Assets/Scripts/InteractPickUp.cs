using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPickUp : MonoBehaviour
{
    private GameObject player;

    public AudioClip itemGetSound;

   public void PickedUp()
    {
       player = GameObject.FindGameObjectWithTag("Player");

        player.GetComponent<Inventory>().items.Add(gameObject.name);
        SoundManager.Instance.Play(itemGetSound);

        Destroy(gameObject);
    } 
   
}
