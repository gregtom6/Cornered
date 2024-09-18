/// <summary>
/// Filename: UIFader.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIFader : MonoBehaviour
{
    private CanvasGroup m_CanvasGroup;
    private bool m_PlayerDied;
    private float m_PlayerDiedStartTime;

    private void Start()
    {
        m_CanvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        EventManager.AddListener<CharacterDefeatedEvent>(OnCharacterDefeated);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<CharacterDefeatedEvent>(OnCharacterDefeated);
    }

    private void OnCharacterDefeated(CharacterDefeatedEvent e)
    {
        if (e.characterType == ECharacterType.Enemy)
        {
            return;
        }

        m_PlayerDied = true;
        m_PlayerDiedStartTime = Time.time;
    }

    private void Update()
    {
        if (!m_PlayerDied)
        {
            return;
        }

        float currentTime = Time.time - m_PlayerDiedStartTime;
        float percentage = currentTime / AllConfig.Instance.TimeConfig.waitTimeUntilGameOver;
        percentage = Mathf.Clamp01(percentage);

        m_CanvasGroup.alpha = percentage;
    }
}
