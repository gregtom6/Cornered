/// <summary>
/// Filename: UIHealthBar.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    [SerializeField] private Image m_CurrentHealthImage;
    [SerializeField] private Image m_DeltaHealthImage;

    private CHealth m_HealthComponent;
    private float m_PreviousHealthFillAmount;

    private void OnEnable()
    {
        EventManager.AddListener<CharacterInitializedEvent>(OnCharacterInitializedEvent);
        EventManager.AddListener<CharacterReceivedShotEvent>(OnCharacterReceivedShot);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<CharacterInitializedEvent>(OnCharacterInitializedEvent);
        EventManager.RemoveListener<CharacterReceivedShotEvent>(OnCharacterReceivedShot);
    }

    private void OnCharacterInitializedEvent(CharacterInitializedEvent characterInitializedEvent)
    {
        if (characterInitializedEvent.characterType == ECharacterType.Enemy)
        {
            return;
        }

        m_HealthComponent = characterInitializedEvent.healthComponent;

        m_CurrentHealthImage.fillAmount = 1f;
        m_DeltaHealthImage.fillAmount = 1f;
        m_PreviousHealthFillAmount = m_CurrentHealthImage.fillAmount;
    }

    private void OnCharacterReceivedShot(CharacterReceivedShotEvent characterReceivedShotEvent)
    {
        if (characterReceivedShotEvent.charType == ECharacterType.Enemy)
        {
            return;
        }

        m_DeltaHealthImage.fillAmount = m_PreviousHealthFillAmount;
    }

    private void Update()
    {
        m_CurrentHealthImage.fillAmount = m_HealthComponent.currentHealth / m_HealthComponent.GetMaxHealth();
        m_PreviousHealthFillAmount = m_CurrentHealthImage.fillAmount;
    }
}