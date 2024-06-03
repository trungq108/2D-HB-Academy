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
    [SerializeField] Transform kunaiPrefab;
    [SerializeField] Transform puzzle;
    [SerializeField] GameObject attackArea;

    bool isGround;
    bool isJumping;
    bool isAttack;
    bool isDead;
    float horizontal;
    Vector3 savePoint;

    public override void OnInit()
    {
        base.OnInit();
        ChangeAnim("idle");
        isAttack = false;
        isJumping = false;
        transform.position = savePoint;
        SavePoint();
        DeActiveAttack();
    }

    public override void Despawn()
    {
        base.Despawn();
        OnInit();
    }

    protected override void OnDead()
    {
        base.OnDead();
    }

    void Update()
    {
        if(IsDead) { return; }

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

            if(Input.GetKeyDown(KeyCode.E))
            {
                Jump();
            }

            if(Input.GetKeyDown(KeyCode.C))
            {
                Attack();
            }

            if(Input.GetKeyDown(KeyCode.V))
            {
                Throw();
            }

            if (Mathf.Abs(horizontal) > Mathf.Epsilon)
            {
                ChangeAnim("run");
            }

        }

        if(!isGround && rb.velocity.y < 0)
        {
            ChangeAnim("fall");
            isJumping = false;
        }

        if (Mathf.Abs(horizontal) > Mathf.Epsilon)
        {
            rb.velocity = new Vector2(horizontal * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
            transform.eulerAngles = new Vector3(0, horizontal > 0 ? 0 : 180, 0);
        }

        else if(isGround && !isAttack)
        {
            ChangeAnim("idle");
            rb.velocity = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        //if (Mathf.Abs(horizontal) > Mathf.Epsilon)
        //{
        //    rb.velocity = new Vector2(horizontal * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
        //    transform.eulerAngles = new Vector3(0, horizontal > 0 ? 0 : 180, 0);
        //}
    }

    public void SetDirection(float direct)
    {
        horizontal = direct;
    }

    public void Throw()
    {
        Instantiate(kunaiPrefab, puzzle.transform.position, puzzle.transform.rotation);
        ChangeAnim("throw");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.3f);

    }

    public void Attack()
    {
        if (isGround)
        {
            ChangeAnim("attack");
            isAttack = true;
            Invoke(nameof(ResetAttack), 0.5f);
            ActiveAttack();
            Invoke(nameof(DeActiveAttack), 0.5f);
        }
    }
    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce);
        ChangeAnim("jump");
        isJumping = true;
    }

    void ResetAttack()
    {
        ChangeAnim("idle");
        isAttack = false;
    }

    void ActiveAttack()
    {
        attackArea.SetActive(true);
    }

    void DeActiveAttack()
    {
        attackArea.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            UIController.Instance.SetCoin(1);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("DeadZone"))
        {
            ChangeAnim("die");
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
