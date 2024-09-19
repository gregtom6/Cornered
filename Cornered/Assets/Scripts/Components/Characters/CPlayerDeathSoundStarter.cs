/// <summary>
/// Filename: CPlayerDeathSoundStarter.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CAudioPlayer))]
public class CPlayerDeathSoundStarter : MonoBehaviour
{
    private bool m_IsPlayerDead;
    private float m_StartTime;
    private CAudioPlayer m_AudioPlayer;

    private void Start()
    {
        m_AudioPlayer = GetComponent<CAudioPlayer>();
    }

    private void OnEnable()
    {
        EventManager.AddListener<CharacterDefeatedEvent>(OnCharacterDefeated);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<CharacterDefeatedEvent>(OnCharacterDefeated);
    }

    private void OnCharacterDefeated(CharacterDefeatedEvent characterDefeatedEvent)
    {
        if (characterDefeatedEvent.characterType == ECharacterType.Enemy)
        {
            return;
        }

        m_IsPlayerDead = true;
        m_StartTime = Time.time;

        m_AudioPlayer.Play();
        m_AudioPlayer.SetVolume(0f);
    }

    private void Update()
    {
        if (!m_IsPlayerDead)
        {
            return;
        }

        float currentTime = Time.time - m_StartTime;
        float percentage = currentTime / AllConfig.Instance.TimeConfig.waitTimeUntilGameOver;
        percentage = Mathf.Clamp01(percentage);

        m_AudioPlayer.SetVolume(percentage);
    }
}
