using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Player"))
        {
            Debug.Log("Hit");
            collision.GetComponent<Character>().OnHit(damage);
        }
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
