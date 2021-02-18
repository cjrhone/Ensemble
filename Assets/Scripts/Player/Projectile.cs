using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 firingPoint;
    
    [SerializeField]
    private float projectileSpeed;

    [SerializeField]
    private float maxProjectileDistance;

    private GameObject triggeringEnemy;
    // private GameObject player;
    public float damage;

    void Start() 
    {
        firingPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveProjectile();
        
    }
    
    void MoveProjectile()
    {
        if(Vector3.Distance(firingPoint, transform.position) > maxProjectileDistance )
        {
            Destroy(this.gameObject);
        } else
        {
        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
        }
    }
     public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            triggeringEnemy = other.gameObject;
            triggeringEnemy.GetComponent<Enemy>().health -= damage;
            Destroy(this.gameObject);
            
        }

        // if(other.tag == "Player")
        // {
        //     Destroy(player);
        // }
    }
}
