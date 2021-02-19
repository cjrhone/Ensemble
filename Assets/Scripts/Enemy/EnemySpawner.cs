using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject theEnemy;
    public int xPos;
    public int zPos;
    public int enemyCount;

    void Start() 
    {
        StartCoroutine(EnemyDrop());

    }

    IEnumerator EnemyDrop()
    {
        while (enemyCount < 10)
        {
            xPos = Random.Range(-8, 8);
            zPos = Random.Range(-10, 10);

            Instantiate(theEnemy, new Vector3(xPos, 0.5f, zPos), Quaternion.identity );
            yield return new WaitForSeconds(0.1f);
            enemyCount += 1;
        }
    }
}
