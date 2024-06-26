/// <summary>
/// Filename: CRedLightController.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CAudioPlayer))]
[RequireComponent(typeof(Animator))]
public class CRedLightController : MonoBehaviour
{
    private static readonly int ANIM_PARAM_ACTIVATE = Animator.StringToHash("activate");

    private Animator m_Animator;
    private CAudioPlayer[] m_AudioPlayers;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_AudioPlayers = GetComponents<CAudioPlayer>();
    }

    private void OnEnable()
    {
        EventManager.AddListener<TimeOverHappenedEvent>(OnTimeOverHappenedEvent);
        EventManager.AddListener<CharacterDefeatedEvent>(OnCharacterDefeatedEvent);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<TimeOverHappenedEvent>(OnTimeOverHappenedEvent);
        EventManager.RemoveListener<CharacterDefeatedEvent>(OnCharacterDefeatedEvent);
    }

    private void OnTimeOverHappenedEvent(TimeOverHappenedEvent ev)
    {
        m_Animator.SetBool(ANIM_PARAM_ACTIVATE, true);

        for (int i = 0; i < m_AudioPlayers.Length; i++)
        {
            m_AudioPlayers[i].Play();
        }
    }

    private void OnCharacterDefeatedEvent(CharacterDefeatedEvent ev)
    {
        m_Animator.SetBool(ANIM_PARAM_ACTIVATE, false);

        for (int i = 0; i < m_AudioPlayers.Length; i++)
        {
            m_AudioPlayers[i].Stop();
        }
    }
}
