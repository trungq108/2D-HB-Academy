using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] float attackRange;
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody2D rb;
    private IState currentState;

    Character target; public Character Target { get { return target; } }

    private void Update()
    {
        if(currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        ChangeState(new IdleState());
    }

    public override void Despawn()
    {
        base.Despawn();
    }

    protected override void OnDead()
    {
        base.OnDead();
    }

    public void ChangeState(IState newState)
    {
        if(currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if(currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    internal void SetTarget(Character character)
    {
        this.target = character;
        if (IsTargetInRange())
        {
            ChangeState(new AttackState());
        }
        else if (target != null)
        {
            ChangeState(new PatronState());
        }
        else
        {
            ChangeState(new IdleState());
        }
    }

    public void Moving()
    {
        ChangAnim("run");
        rb.velocity = transform.right * moveSpeed * Time.deltaTime;
    }

    public void StopMoving()
    {
        ChangAnim("idle");
        rb.velocity = Vector2.zero;
    }

    public void Attack()
    {

    }

    bool IsTargetInRange()
    {
        return Vector3.Distance(target.transform.position, transform.position) <= attackRange;
    }


}
