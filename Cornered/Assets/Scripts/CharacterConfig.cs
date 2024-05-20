using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterConfig")]
public class CharacterConfig : ScriptableObject
{
    [SerializeField] float m_RunSpeed;
    [SerializeField] float m_EnemyRunSpeed;
    [SerializeField] float m_HeadMaxRotX;
    [SerializeField] float m_HeadMinRotX;
    [SerializeField] float m_HeadRotSpeed;
    [SerializeField] float m_MinHealth;
    [SerializeField] float m_MaxHealth;
    [SerializeField] float m_WaitUntilHealthReloadStarts;
    [SerializeField] float m_HealHealthDelta;

    public float enemyRunSpeed => m_EnemyRunSpeed;
    public float runSpeed => m_RunSpeed;
    public float headMaxRotX => m_HeadMaxRotX;
    public float headMinRotX => m_HeadMinRotX;
    public float headRotSpeed => m_HeadRotSpeed;
    public float healHealthDelta => m_HealHealthDelta;
    public float minHealth => m_MinHealth;
    public float maxHealth => m_MaxHealth;
    public float waitUntilHealthReloadStarts => m_WaitUntilHealthReloadStarts;
}
