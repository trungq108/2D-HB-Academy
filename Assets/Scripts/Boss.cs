using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : Enemy
{
    public override void ActiveAttack()
    {
        Throw();
    }

    void Throw()
    {
        Debug.Log("Boss Attack");
    }
}
