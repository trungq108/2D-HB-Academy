using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{    
    [SerializeField] Animator animator;

    string currentAnimName;
    int hp;

    public bool IsDead => hp == 0;

    private void Start()
    {
        OnInit();
    }

    public virtual void OnInit()
    {
        hp = 100;
    }

    public virtual void Despawn()
    {

    }

    protected virtual void OnDead()
    {
        ChangeAnim("die");
        Invoke(nameof(Despawn) , 2f);
    }

    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            animator.ResetTrigger(animName);
            currentAnimName = animName;
            animator.SetTrigger(currentAnimName);
        }
    }

    public void OnHit(int damage)
    {
        hp = Mathf.Clamp(hp, 0, hp - damage);
        if(IsDead)
        {
            OnDead();
        }
    }
}
