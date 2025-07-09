using UnityEngine;

[CreateAssetMenu(fileName = "AntStats", menuName = "Scriptable Objects/AntStats")]
public class AntStats : ScriptableObject
{
    public float health;
    public float speed;
    public float damage;
    public float range;
    public float attackRate;
}
