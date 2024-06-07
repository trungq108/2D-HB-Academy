using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackState : IState
{
    float timer;

    public void OnEnter(Enemy enemy)
    {
        if (enemy.Target != null)
        {
            enemy.ChangeDirection(enemy.Target.transform.position.x > enemy.transform.position.x);
            enemy.StopMoving();
            enemy.RangeAttack();
        }
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if (timer > 0.5f)
        {
            enemy.ChangeState(new PatronState());
        }
    }

    public void OnExit(Enemy enemy)
    {

    }
}
