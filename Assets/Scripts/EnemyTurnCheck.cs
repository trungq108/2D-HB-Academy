using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnCheck : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Enemy enemy;

    private void Update()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position , Vector2.down, 3f, groundLayer);
        //if(hit.collider == null)
        //{
        //    enemy.ChangeDirection(!enemy.IsRight);
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == groundLayer)
        {
            Debug.Log("Turn");
            enemy.ChangeDirection(!enemy.IsRight);
        }
    }
}
