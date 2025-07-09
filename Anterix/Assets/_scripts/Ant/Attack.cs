using System;
using JetBrains.Annotations;
using UnityEngine;

public class Attack : MonoBehaviour
{

    [Header("Stats")]
    public float attackRate;
    public float range;
    public float damage;
    
    [Header("Flags")]
    public bool canAttack;
    public bool isEnemy;

    // trackers
    private float attackCooldown;
    
    private void Update()
    {
        GameObject closestEnemy = FindClosestObjectToAttack();
        canAttack = closestEnemy;
        if (attackCooldown <= 0 && canAttack)
        {
            AttackEnemy(closestEnemy);
            attackCooldown = 1 / attackRate;
        }

        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
    }


    void AttackEnemy(GameObject target)
    {
        Health enemyHealth = target.GetComponent<Health>();
        if (enemyHealth)
            enemyHealth.TakeDamage(damage);
    }

    [CanBeNull]
    GameObject FindClosestObjectToAttack()
    {
        Vector2 direction = Vector2.right;
        LayerMask targetLayer = LayerMask.GetMask("Enemy");
        if (isEnemy)
        {
            direction = Vector2.left;
            targetLayer = LayerMask.GetMask("Ant");
        }
        Vector2 center = (Vector2)transform.position + direction * (range / 2f);
        Vector2 size = new Vector2(range, 2);
        float angle = 0f;

        Collider2D[] hits = Physics2D.OverlapBoxAll(center, size, angle, targetLayer);

        Collider2D closest = null;
        float minDist = Mathf.Infinity;

        foreach (var hit in hits)
        {
            float dist = Vector2.Distance(transform.position, hit.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = hit;
            }
        }
        if (!closest) return null;
        return closest.gameObject;
    }
    
    
    // Optional: visualize the box in the editor
    void OnDrawGizmosSelected()
    {
        Vector2 direction = Vector2.right;
        if (isEnemy)
            direction = Vector2.left;
        Vector2 center = (Vector2)transform.position + direction * (range / 2f);
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(center, new Vector3(range, 2, 0));
    }
}
