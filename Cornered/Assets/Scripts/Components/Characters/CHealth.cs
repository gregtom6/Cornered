using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CHealth : MonoBehaviour
{
    public float currentHealth => m_CurrentHealth;

    protected float m_CurrentHealth;

    protected float m_HealingMeasurementTimeStart;

    public void DamageHealth(float damage)
    {
        m_CurrentHealth = Mathf.Clamp(m_CurrentHealth += damage, AllConfig.Instance.CharacterConfig.minHealth, GetMaxHealth());
        m_HealingMeasurementTimeStart = Time.time;

        if (m_CurrentHealth <= AllConfig.Instance.CharacterConfig.minHealth)
        {
            EventManager.Raise(new CharacterDefeatedEvent { characterType = GetCharacterType() });
        }
    }

    protected abstract ECharacterType GetCharacterType();

    public abstract float GetMaxHealth();

    protected abstract float GetReloadWaitingMaxTime();

    protected void Start()
    {
        m_CurrentHealth = GetMaxHealth();
        EventManager.Raise(new CharacterInitializedEvent { characterType = GetCharacterType(), healthComponent = this });
    }

    protected void Update()
    {
        if (m_CurrentHealth >= GetMaxHealth() ||
            m_CurrentHealth <= AllConfig.Instance.CharacterConfig.minHealth)
        {
            return;
        }

        float currentTime = Time.time - m_HealingMeasurementTimeStart;
        if (currentTime >= GetReloadWaitingMaxTime())
        {
            m_CurrentHealth = Mathf.Clamp(m_CurrentHealth += AllConfig.Instance.CharacterConfig.healHealthDelta * Time.deltaTime, AllConfig.Instance.CharacterConfig.minHealth, GetMaxHealth());
        }
    }
}
