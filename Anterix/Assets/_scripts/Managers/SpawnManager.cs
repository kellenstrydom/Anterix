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
            if (CheckSpawn())
            {
                SpawnAnt();
                spawnTimer = 0;
            }
        }
        else
        {
            spawnTimer += Time.deltaTime;
        }
    }

    bool CheckSpawn()
    {
        Vector2 direction = Vector2.right;
        LayerMask targetLayer = LayerMask.GetMask("Ant");
        
        Vector2 center = (Vector2)transform.position + direction * (1 / 2f);
        Vector2 size = new Vector2(1, 2);
        float angle = 0f;

        Collider2D hit = Physics2D.OverlapBox(center, size, angle, targetLayer);
        
        return !hit;
    }

    void SpawnAnt()
    {
        float posVariance = Random.Range(-varianceRange, varianceRange);
        Vector3 spawnPos = new Vector3(spawnPoint.position.x, spawnPoint.position.y + posVariance, spawnPoint.position.z + posVariance);
        Instantiate(ant, spawnPos, quaternion.identity);
    }
}
