using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject bloodVFX;
    [SerializeField] float speed = 20f;

    private int damage;

    private void Start()
    {
        OnInit();
        Invoke(nameof(OnDespawn), 5f);
    }

    public void OnInit()
    {
        rb.velocity = transform.right * speed;
    }
    
    public void OnDespawn()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Instantiate(bloodVFX, transform.position, transform.rotation);
            collision.GetComponent<Character>().OnHit(damage);
            OnDespawn();
        }
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
