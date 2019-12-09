using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public Transform[] positions;
    public GameObject[] enemies;
    void Start()
    {
        foreach(Transform t in positions)
        {
            Instantiate(enemies[Random.Range(0, enemies.Length)], t.position, Quaternion.identity);
        }
    }

    
}
