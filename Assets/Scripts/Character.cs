using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{    
    [SerializeField] Animator animator;
    [SerializeField] protected HeathBar heathBar;
    [SerializeField] protected CombatText combatTextPrefab;
    [SerializeField] private int maxHp = 100;

    string currentAnimName;
    int hp;

    public bool IsDead => hp <= 0;

    private void Start()
    {
        heathBar.OnInit(maxHp, this.transform);
        OnInit();
    }

    public virtual void OnInit()
    {
        hp = maxHp;
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
        if(hp <= 0)
        {
            OnDead();
        }

        Instantiate(combatTextPrefab, transform.position + Vector3.up, Quaternion.identity).OnInit(damage);
        heathBar.SetHp(hp);
    }

    public void Healing(float helthPercent)
    {
        hp = Mathf.Clamp(hp, (int)(hp + maxHp * helthPercent), maxHp);
        heathBar.SetHp(hp);
    }
}
