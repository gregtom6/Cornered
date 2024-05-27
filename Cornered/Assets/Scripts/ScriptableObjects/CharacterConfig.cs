using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterConfig")]
public class CharacterConfig : ScriptableObject
{
    [SerializeField] private float m_RunSpeed;
    [SerializeField] private float m_EnemyRunSpeed;
    [SerializeField] private float m_HeadMaxRotX;
    [SerializeField] private float m_HeadMinRotX;
    [SerializeField] private float m_HeadRotSpeed;
    [SerializeField] private float m_MinHealth;
    [SerializeField] private float m_MaxHealth;
    [SerializeField] private float m_EnemyMaxHealh;
    [SerializeField] private float m_WaitUntilHealthReloadStarts;
    [SerializeField] private float m_EnemyWaitUntilHealthReloadStarts;
    [SerializeField] private float m_HealHealthDelta;
    [SerializeField] private float m_FastBootsSpeedMultiplier;
    [SerializeField] private GameObject m_EnemyPrefab;

    public float enemyRunSpeed => m_EnemyRunSpeed;
    public float runSpeed => m_RunSpeed;
    public float headMaxRotX => m_HeadMaxRotX;
    public float headMinRotX => m_HeadMinRotX;
    public float headRotSpeed => m_HeadRotSpeed;
    public float healHealthDelta => m_HealHealthDelta;
    public float minHealth => m_MinHealth;
    public float maxHealth => m_MaxHealth;

    public float enemyMaxHealth => m_EnemyMaxHealh;
    public float waitUntilHealthReloadStarts => m_WaitUntilHealthReloadStarts;

    public float enemyWaitUntilHealthReloadStarts => m_EnemyWaitUntilHealthReloadStarts;

    public float fastBootsSpeedMultiplier => m_FastBootsSpeedMultiplier;
    public GameObject enemyPrefab => m_EnemyPrefab;
}
