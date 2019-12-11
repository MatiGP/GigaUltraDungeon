using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public Transform[] positions;
    public GameObject[] enemies;
    public GameObject[] bosses;
    public Transform doorSpawnPoint;
    public Transform playerSpawnPoint;

    public bool spawnBoss;
    

    public void Spawn()
    {
        if (spawnBoss)
        {
            Instantiate(bosses[Random.Range(0, bosses.Length)], positions[Random.Range(0, positions.Length)].position, Quaternion.identity);

        }
        else
        {
            foreach(Transform t in positions)
            {
                Instantiate(enemies[Random.Range(0, enemies.Length)], t.position, Quaternion.identity);
            }
        }
    }

    

    
}
