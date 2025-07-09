using System;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyBehaviour : MonoBehaviour
{   
    
    [Header("Components")]
    Movement _movement;
    Health _health;
    private Attack _attack;
    
    [FormerlySerializedAs("antStats")] [Header("Stats")]
    public EnemyStats enemyStats;
    public AntBehaviour.State currentState = AntBehaviour.State.Moving;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _health = GetComponent<Health>();
        _attack = GetComponent<Attack>();
        
        SetStats();
    }

    void SetStats()
    {
        // attack
        if (_attack)
        {
            _attack.range = enemyStats.range;
            _attack.damage = enemyStats.damage;
            _attack.attackRate = enemyStats.attackRate;
            _attack.isEnemy = true;
        }      
        
        // health
        if (_health)
        {
            _health.health = enemyStats.health;
        }        
        
        // movement
        if (_movement)
        {
            _movement.speed = -enemyStats.speed;
        }    
    }


    private void Update()
    {
        CheckCanAttack();
    }

    void CheckCanAttack()
    {
        if (_attack.canAttack && currentState == AntBehaviour.State.Moving)
        {
            ChangeState(AntBehaviour.State.Attaking);
        }

        if (!_attack.canAttack && currentState == AntBehaviour.State.Attaking)
        {
            ChangeState(AntBehaviour.State.Moving);
        }
    }

    void ChangeState(AntBehaviour.State state)
    {
        currentState = state;
        switch (currentState)
        {
            case AntBehaviour.State.Moving:
                StartMove();
                break;
            case AntBehaviour.State.Attaking:
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
}

