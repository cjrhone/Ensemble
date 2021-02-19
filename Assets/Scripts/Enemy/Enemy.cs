using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Variables
    public float health;
    public float pointsToGive;

    public GameObject bullet;
    public GameObject bulletSpawnPoint;
    private Transform bulletSpawned;

    public float waitTime;

    private float currentTime;

    private bool shot;

    //Methods

    public void Update()
    {
        if(health <= 0)
        {
            Die();
        }

        this.transform.LookAt(PlayerController.Instance.transform);

        if(currentTime == 0)
        {
            Shoot();

        }

        if(shot && currentTime < waitTime)
        {
            currentTime += 1 * Time.deltaTime;
        }

        if (currentTime >= waitTime)
        {
            currentTime = 0;
        }
    }

    public void Die()
    {
        print("Enemy " + this.gameObject.name + " has died!");
        Destroy(this.gameObject);

        PlayerController.Instance.points += pointsToGive;
    }

    public void Shoot()
    {
        shot = true;
        
        bulletSpawned = Instantiate(bullet.transform, bulletSpawnPoint.transform.position, Quaternion.identity);
        bulletSpawned.rotation = this.transform.rotation;

    }

}
