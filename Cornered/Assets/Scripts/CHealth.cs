using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHealth : MonoBehaviour
{
    [SerializeField] private ECharacterType characterType = ECharacterType.Count;

    public float currentHealth => m_CurrentHealth;

    private float m_CurrentHealth;

    private float m_HealingMeasurementTimeStart;

    public void DamageHealth(float damage)
    {
        m_CurrentHealth = Mathf.Clamp(m_CurrentHealth += damage, AllConfig.Instance.CharacterConfig.minHealth, AllConfig.Instance.CharacterConfig.maxHealth);
        m_HealingMeasurementTimeStart = Time.time;
    }
    private void Start()
    {
        m_CurrentHealth = AllConfig.Instance.CharacterConfig.maxHealth;
        EventManager.Raise(new CharacterInitializedEvent { characterType = characterType, healthComponent = this });
    }

    private void Update()
    {
        if (m_CurrentHealth >= AllConfig.Instance.CharacterConfig.maxHealth ||
            m_CurrentHealth <= AllConfig.Instance.CharacterConfig.minHealth)
        {
            return;
        }

        float currentTime = Time.time - m_HealingMeasurementTimeStart;
        if (currentTime >= AllConfig.Instance.CharacterConfig.waitUntilHealthReloadStarts)
        {
            m_CurrentHealth = Mathf.Clamp(m_CurrentHealth += AllConfig.Instance.CharacterConfig.healHealthDelta * Time.deltaTime, AllConfig.Instance.CharacterConfig.minHealth, AllConfig.Instance.CharacterConfig.maxHealth);
        }
    }
}
