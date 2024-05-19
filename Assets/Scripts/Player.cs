using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : Character
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float moveSpeed = 250f;
    [SerializeField] float jumpForce = 400f;
    [SerializeField] LayerMask groundLayerMask;

    bool isGround;
    bool isJumping;
    bool isAttack;
    bool isDead;
    Vector3 savePoint;


    private void Start()
    {
        SavePoint();
    }

    public override void OnInit()
    {
        base.OnInit();
        ChangAnim("idle");
        isDead = false;
        isAttack = false;
        isJumping = false;
        transform.position = savePoint;
    }

    public override void Despawn()
    {
        base.Despawn();
    }

    protected override void OnDead()
    {
        base.OnDead();
    }

    void FixedUpdate()
    {
        if (isDead) { return; }

        isGround = CheckGround();
        float horizontal = Input.GetAxisRaw("Horizontal");

        if(isAttack)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if(isGround)
        {
            if (isJumping) return;

            if(Input.GetKeyDown(KeyCode.E) && isGround)
            {
                Jump();
            }

            if(Input.GetKeyDown(KeyCode.C) && isGround)
            {
                Attack();
            }
            if(Input.GetKeyDown(KeyCode.V) && isGround)
            {
                Throw();
            }

            if (Mathf.Abs(horizontal) > Mathf.Epsilon)
            {
                ChangAnim("run");
            }

        }

        if(!isGround && rb.velocity.y < 0)
        {
            ChangAnim("fall");
            isJumping = false;
        }

        if(Mathf.Abs(horizontal) > Mathf.Epsilon)
        {
            rb.velocity = new Vector2(horizontal*moveSpeed*Time.fixedDeltaTime, rb.velocity.y);
            transform.eulerAngles = new Vector3(0, horizontal > 0 ? 0:180, 0);
        }

        else if(isGround)
        {
            ChangAnim("idle");
            rb.velocity = Vector2.zero;
        }
    }


    void Throw()
    {
        ChangAnim("throw");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.3f);

    }
    void Attack()
    {
        ChangAnim("attack");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.3f);

    }

    void ResetAttack()
    {
        ChangAnim("idle");
        isAttack = false;
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce);
        ChangAnim("jump");
        isJumping = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("DeadZone"))
        {
            isDead = true;
            ChangAnim("die");
            Invoke(nameof(OnInit), 0.5f);
        }
    }

    public void SavePoint()
    {
        savePoint = transform.position;
    }

    bool CheckGround()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.1f, Color.yellow);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayerMask);
        return hit.collider != null;
    }

}
