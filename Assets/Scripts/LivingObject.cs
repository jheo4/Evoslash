using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LivingObject : MonoBehaviour, IDamageable
{
    public float maxHP = 100f;
    public float HP {get; protected set;}
    public bool isLive {get; protected set;}
    public event Action onDeath;
    protected virtual void OnEnable()
    {
        isLive = true;
        HP = maxHP;
    }


    public virtual void OnHit(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        HP -= damage;
        if(HP <= 0 && isLive) {
            HP = 0;
            Die();
        }
    }


    public virtual void Heal(float healingPoint)
    {
        if(!isLive) return;
        HP += healingPoint;
        if(HP >= maxHP) HP = maxHP;
    }


    public virtual void Die()
    {
        isLive = false;
        if(onDeath != null) onDeath();
    }
}

