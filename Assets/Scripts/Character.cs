using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{    
    [SerializeField] Animator animator;

    string currentAnimName;
    float hp;

    public bool IsDead => hp <= 0;

    private void Start()
    {
        OnInit();
    }

    public virtual void OnInit()
    {
        hp = 100f;
    }

    public virtual void Despawn()
    {

    }

    protected virtual void OnDead()
    {

    }

    protected void ChangAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            animator.ResetTrigger(animName);
            currentAnimName = animName;
            animator.SetTrigger(currentAnimName);
        }
    }

    public void OnHit(float damage)
    {
        if(hp > damage)
        {
            hp -= damage;
            if (IsDead)
            {
                OnDead();
            }
        }
    }


}
