using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Variables
    public float health;
    public float pointsToGive;

    public GameObject player;

    //Methods
    public void Update()
    {
        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        print("Enemy " + this.gameObject.name + " has died!");
        Destroy(this.gameObject);

        player.GetComponent<PlayerController>().points += pointsToGive;
    }

}
