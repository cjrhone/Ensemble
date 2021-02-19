using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicOrbPowerup : MonoBehaviour
{
    // public GameObject pickupEffect;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Pickup(other);
        }
    }

    void Pickup(Collider player)
    {
        Debug.Log("Power up picked up!");

        // Spawn cool effect
        // Instantiate(pickupEffect, transform.position, transform.rotation);

        //Apply effect to player

        //Remove power up object
        Destroy(gameObject);
    }
  
}
