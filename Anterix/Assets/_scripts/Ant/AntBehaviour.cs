using System;
using UnityEngine;

public class AntBehaviour : MonoBehaviour
{   
    public enum State
    {
        Moving = 0,
        Attaking = 1,
    }
    
    [Header("Components")]
    Movement _movement;
    Health _health;
    private Attack _attack;
    
    [Header("Stats")]
    public AntStats antStats;
    public State currentState = State.Moving;

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

    void ChangeState(State state)
    {
        currentState = state;
        switch (currentState)
        {
            case State.Moving:
                StartMove();
                break;
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
}
