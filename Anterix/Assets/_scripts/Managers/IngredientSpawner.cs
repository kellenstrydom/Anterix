using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class IngredientSpawner : MonoBehaviour
{

    [Header("Spawn Area (relative to this object)")]
    public Vector2 areaSize = new Vector2(5f, 3f);
    public Vector2 areaOffset = Vector2.zero;

    [Header("Spawning")] 
    public bool isSpawning = true;
    public GameObject ingredientPrefab;
    public float spawnRate = 1f;
    
    // tracking
    float spawnCooldown = 0f;

    private void Update()
    {
        if (spawnCooldown <= 0 && isSpawning)
        {
            SpawnObjects();
            spawnCooldown = 1 / spawnRate;
        }

        if (spawnCooldown > 0)
        {
            spawnCooldown -= Time.deltaTime;
        }
    }

    [ContextMenu("Spawn Objects")]
    public void SpawnObjects()
    {
        if (ingredientPrefab == null) return;
        
        Vector2 spawnPos = GetRandomPointInRectangle();
        Instantiate(ingredientPrefab, spawnPos, Quaternion.identity);
    }

    private Vector2 GetRandomPointInRectangle()
    {
        float x = 0, y = 0;
        bool isValid = false;
        float minDistance = 1f; // Minimum allowed distance from other objects
        LayerMask interactableLayer = LayerMask.GetMask("Interactable");

        while (!isValid)
        {
            Vector2 min = (Vector2)transform.position + areaOffset - areaSize / 2f;
            Vector2 max = (Vector2)transform.position + areaOffset + areaSize / 2f;

            x = Random.Range(min.x, max.x);
            y = Random.Range(min.y, max.y);
            Vector2 spawnPoint = new Vector2(x, y);

            // Check if there's any collider nearby on the Interactable layer
            Collider2D hit = Physics2D.OverlapCircle(spawnPoint, minDistance, interactableLayer);

            if (hit == null)
            {
                isValid = true; // Nothing nearby, it's safe to spawn here
            }
        }

        return new Vector2(x, y);
    }

    // Visualize the spawn area in Scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 center = transform.position + (Vector3)areaOffset;
        Gizmos.DrawWireCube(center, areaSize);
    }
}
