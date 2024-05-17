using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealthSystem : MonoBehaviour
{
    public static Action MonsterDeadAction;
    public int m_maxHP = 30;

    private int m_hp;

    private void Start()
    {
        m_hp = m_maxHP;
    }
    public void TakeDamage(int damage)
    {
        m_hp -= damage;
        if (m_hp <= 0)
        {
            MonsterDeadAction?.Invoke();
            Destroy(gameObject);
        }
    }
}
