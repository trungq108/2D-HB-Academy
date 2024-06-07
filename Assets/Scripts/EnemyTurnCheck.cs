using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnCheck : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Enemy enemy;
    private bool IsRight;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == groundLayer)
        {
            IsRight = enemy.IsRight;
            enemy.ChangeDirection(!IsRight);
        }
    }
}
