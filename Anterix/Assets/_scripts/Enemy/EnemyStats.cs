using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Scriptable Objects/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public float health;
    public float speed;
    public float damage;
    public float range;
    public float attackRate;
}
