using System;
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
    [SerializeField] Kunai kunaiPrefab;
    [SerializeField] int kunaiIndex;
    [SerializeField] Transform puzzle;
    [SerializeField] AttackArea attackArea;
    [SerializeField] private int strength = 30;
    [SerializeField] float kunaiCooldown = 10f;

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
        kunaiIndex = 3;
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

            if(Input.GetKeyDown(KeyCode.V) && kunaiIndex > 0)
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

    public void SetDirection(float direct)
    {
        horizontal = direct;
    }

    public void Throw()
    {
        if (kunaiIndex <= 0) return;
        if (isAttack) return;

        kunaiIndex--;
        Kunai newKunai = Instantiate(kunaiPrefab, puzzle.transform.position, puzzle.transform.rotation);
        newKunai.SetDamage(strength);
        UIController.Instance.InitTextBullet(kunaiIndex);
        ChangeAnim("throw");
        isAttack = true;
        if (kunaiIndex <= 0)
        {
            Invoke(nameof(ResetKunai), kunaiCooldown);
        }

        Invoke(nameof(ResetAttack), 0.3f);
    }

    private void ResetKunai()
    {
        kunaiIndex = 3;
        UIController.Instance.InitTextBullet(kunaiIndex);
    }

    public void Attack()
    {
        if(isAttack) return;

        isAttack = true;
        
        ChangeAnim("attack");
        ActiveAttack();
        Invoke(nameof(ResetAttack), 1f);
        Invoke(nameof(DeActiveAttack), 0.5f);
    }

    public void Jump()
    {
        if(!isGround) return;
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
        attackArea.SetDamage(strength);
        attackArea.gameObject.SetActive(true);
    }

    void DeActiveAttack()
    {
        attackArea.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            UIController.Instance.SetCoin(1);
            BuffDamage(5);
            Debug.Log("get coin");
            Destroy(collision.gameObject);

        }
        if (collision.CompareTag("DeadZone"))
        {
            ChangeAnim("die");
            Invoke(nameof(OnInit), 0.5f);
        }
        if (collision.gameObject.name == "HealthPotion")
        {
            Debug.Log("healed");
            Healing(0.3f);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.name == "WaterPotion")
        {
            Debug.Log("water");
            WaterSetting.Instance.WaterOn();
            Destroy(collision.gameObject);
        }

    }

    private void BuffDamage(int percent)
    {
        int newStreng = strength * percent / 100;
        Debug.Log(newStreng);
        strength += newStreng;
        Debug.Log(strength);
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
