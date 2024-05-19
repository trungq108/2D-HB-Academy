using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed = 20f;

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
            Debug.Log("Shot");
            collision.GetComponent<Character>().OnHit(30);
            OnDespawn();
        }
    }

}
