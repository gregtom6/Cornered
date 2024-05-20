using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    [SerializeField] private Image m_CurrentHealthImage;

    private CHealth m_HealthComponent;

    private void OnEnable()
    {
        EventManager.AddListener<CharacterInitializedEvent>(OnCharacterInitializedEvent);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<CharacterInitializedEvent>(OnCharacterInitializedEvent);
    }

    private void OnCharacterInitializedEvent(CharacterInitializedEvent characterInitializedEvent)
    {
        if (characterInitializedEvent.characterType == ECharacterType.Enemy)
        {
            return;
        }

        m_HealthComponent = characterInitializedEvent.healthComponent;
    }

    private void Update()
    {
        m_CurrentHealthImage.fillAmount = m_HealthComponent.currentHealth / AllConfig.Instance.CharacterConfig.maxHealth;
    }
}

public struct CharacterInitializedEvent
{
    public ECharacterType characterType;
    public CHealth healthComponent;
}