using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnCheck : MonoBehaviour
{
    [SerializeField] Enemy enemy;

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemy.ChangeDirection(true);
    }
}
