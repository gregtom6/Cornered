using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CRedLightController : MonoBehaviour
{
    protected static readonly int ANIM_PARAM_ACTIVATE = Animator.StringToHash("activate");

    private Animator m_Animator;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
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
    }

    private void OnCharacterDefeatedEvent(CharacterDefeatedEvent ev)
    {
        m_Animator.SetBool(ANIM_PARAM_ACTIVATE, false);
    }
}
