using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTileObject : MonoBehaviour
{
    public GameObject[] tilePrefabs;

    private void Start()
    {
        int random = Random.Range(0, tilePrefabs.Length);
        GameObject instance = Instantiate(tilePrefabs[random], transform.position, Quaternion.identity);
        instance.transform.parent = transform;
    }
}
