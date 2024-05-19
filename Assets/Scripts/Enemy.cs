using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] float attackRange = 1f;
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody2D rb;
    private IState currentState;
    bool isRight = true;
    [SerializeField] GameObject attackArea;

    Character target; public Character Target => target;

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
        DeActiveAttack();
    }

    public override void Despawn()
    {
        base.Despawn();
        Destroy(gameObject);
    }

    protected override void OnDead()
    {
        ChangeState(null);
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

    public void SetTarget(Character character)
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
        ChangeAnim("run");
        rb.velocity = transform.right * moveSpeed * Time.deltaTime;
    }

    public void StopMoving()
    {
        ChangeAnim("idle");
        rb.velocity = Vector2.zero;
    }

    public void Attack()
    {
        ChangeAnim("attack");
        ActiveAttack();
        Invoke(nameof(DeActiveAttack), 0.5f);
    }

    public bool IsTargetInRange()
    { 
        if(target != null && Vector3.Distance(target.transform.position, transform.position) <= attackRange)
        {
            return true;
        }
        else { return false; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyWall"))
        {
            ChangeDirection(!isRight);
        }
    }

    public void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;
        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
    }
    void ActiveAttack()
    {
        attackArea.SetActive(true);
    }

    void DeActiveAttack()
    {
        attackArea.SetActive(false);
    }
}
