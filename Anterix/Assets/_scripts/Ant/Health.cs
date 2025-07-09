using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;

    public float GetHealth()
    {
        return health;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        StartCoroutine(DamageEffect());
        if (health <= 0)
        {
            Die();
        }
        
    }

    IEnumerator DamageEffect()
    {
        Color preColor = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        
        GetComponent<SpriteRenderer>().color = preColor;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
