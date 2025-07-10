using System;
using UnityEngine;

public class AntBehaviour : MonoBehaviour
{   
    public enum State
    {
        Moving = 0,
        Waiting = 1,
        Attaking = 2,
    }

    
    [Header("Components")]
    Movement _movement;
    Health _health;
    private Attack _attack;
    
    [Header("Stats")]
    public AntStats antStats;
    public State currentState = State.Moving;
    
    [Header("Flags")]
    public bool gotPotion = false;

    [Header("Other")] 
    public float lineSpace;
    
    [Header("References")]
    PotionManager _potionManager;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _health = GetComponent<Health>();
        _attack = GetComponent<Attack>();
        
        SetStats();
        
        _potionManager = GameObject.Find("Potion Manager").GetComponent<PotionManager>();

    }

    void SetStats()
    {
        // attack
        _attack.range = antStats.range;
        _attack.damage = antStats.damage;
        _attack.attackRate = antStats.attackRate;
        
        // health
        _health.health = antStats.health;
        
        // movement
        _movement.speed = antStats.speed;
    }


    private void Update()
    {
        if (!gotPotion)
        {
            if (_potionManager.potion.position.x < transform.position.x)
            {
                ChangeState(State.Waiting);
            }
            else
            {
                CheckLine();
            }
        }
        CheckCanAttack();
    }

    void CheckCanAttack()
    {
        if (_attack.canAttack && currentState == State.Moving)
        {
            ChangeState(State.Attaking);
        }

        if (!_attack.canAttack && currentState == State.Attaking)
        {
            ChangeState(State.Moving);
        }
    }

    void CheckLine()
    {
        Vector2 direction = Vector2.right;
        LayerMask targetLayer = LayerMask.GetMask("Ant");
        
        Vector2 center = (Vector2)transform.position + direction * (lineSpace);
        float angle = 0f;

        Collider2D hit = Physics2D.Raycast(center + Vector2.up*1, Vector2.down, 2, targetLayer).collider;
            
        if (hit) Debug.Log(hit);
        if (hit) ;

        ChangeState(hit ? State.Waiting : State.Moving);
    }

    void ChangeState(State state)
    {
        if (currentState == state) return;
        
        currentState = state;
        switch (currentState)
        {
            case State.Moving:
                StartMove();
                break;
            case State.Waiting:
            case State.Attaking:
                StopMove();
                break;
        }
    }

    void StartMove()
    {
        _movement.enabled = true;
    }

    void StopMove()
    {
        _movement.enabled = false;
    }
    
    
    // Optional: visualize the box in the editor
    void OnDrawGizmosSelected()
    {
        Vector2 direction = Vector2.right;
        Vector2 center = (Vector2)transform.position + direction * (lineSpace);
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(center, new Vector3(lineSpace/2, 2, 0));
    }
}
