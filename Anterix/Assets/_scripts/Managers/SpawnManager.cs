using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Script : MonoBehaviour
{
    
    public Transform spawnPoint;
    public float varianceRange;
    
    public float spawnRate;
    
    public float spawnTimer;
    public GameObject ant;

    private void Update()
    {
        if (spawnTimer > 1 / spawnRate)
        {
            SpawnAnt();
            spawnTimer = 0;
        }
        else
        {
            spawnTimer += Time.deltaTime;
        }
    }

    void SpawnAnt()
    {
        float posVariance = Random.Range(-varianceRange, varianceRange);
        Vector3 spawnPos = new Vector3(spawnPoint.position.x, spawnPoint.position.y + posVariance, spawnPoint.position.z + posVariance);
        Instantiate(ant, spawnPos, quaternion.identity);
    }
}
