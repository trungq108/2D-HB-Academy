//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;

//public class Boss : Character
//{
//    [SerializeField] float attackRange = 1f;
//    [SerializeField] float moveSpeed;
//    [SerializeField] Rigidbody2D rb;
//    [SerializeField] CapsuleCollider2D capsuleCollider;

//    private int damage;
//    private IState currentState;
//    bool isRight = true; public bool IsRight => isRight;

//    [SerializeField] GameObject attackArea;
//    [SerializeField] LootBag lootBag;

//    Character target; public Character Target => target;

//    private void Update()
//    {
//        if (currentState != null && !IsDead)
//        {
//            currentState.OnExecute(this);
//        }
//    }

//    public override void OnInit()
//    {
//        base.OnInit();
//        ChangeState(new IdleState());
//        DeActiveAttack();
//    }

//    public override void Despawn()
//    {
//        base.Despawn();
//        Destroy(heathBar.gameObject);
//        Destroy(gameObject);
//    }

//    protected override void OnDead()
//    {
//        // capsuleCollider.enabled = false;
//        lootBag.InstantiateItem(this.transform.position);
//        ChangeState(null);
//        base.OnDead();
//    }

//    public void ChangeState(IState newState)
//    {
//        if (currentState != null)
//        {
//            currentState.OnExit(this);
//        }

//        currentState = newState;

//        if (currentState != null)
//        {
//            currentState.OnEnter(this);
//        }
//    }

//    public void SetTarget(Character character)
//    {
//        this.target = character;
//        if (IsTargetInRange())
//        {
//            ChangeState(new AttackState());
//        }
//        else if (target != null)
//        {
//            ChangeState(new PatronState());
//        }
//        else
//        {
//            ChangeState(new IdleState());
//        }
//    }

//    public void Moving()
//    {
//        ChangeAnim("run");
//        rb.velocity = transform.right * moveSpeed * Time.deltaTime;
//    }

//    public void StopMoving()
//    {
//        ChangeAnim("idle");
//        rb.velocity = Vector2.zero;
//    }

//    public void Attack()
//    {
//        ChangeAnim("attack");
//        ActiveAttack();
//        Invoke(nameof(DeActiveAttack), 0.5f);
//    }

//    public bool IsTargetInRange()
//    {
//        if (target != null && Vector3.Distance(target.transform.position, transform.position) <= attackRange)
//        {
//            return true;
//        }
//        else { return false; }
//    }

//    public void ChangeDirection(bool isRight)
//    {
//        this.isRight = isRight;
//        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
//    }

//    public virtual void ActiveAttack()
//    {
//        attackArea.SetActive(true);
//    }

//    void DeActiveAttack()
//    {
//        attackArea.SetActive(false);
//    }

//}
