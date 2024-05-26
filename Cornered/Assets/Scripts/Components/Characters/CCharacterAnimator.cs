using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCharacterAnimator : MonoBehaviour
{
    private static readonly int ANIM_PARAM_LEGSTATE = Animator.StringToHash("legState");

    [SerializeField] private Animator m_Animator;
    private CCharacterController m_CharacterController;

    private void Start()
    {
        m_CharacterController = GetComponent<CCharacterController>();
    }

    private void Update()
    {
        m_Animator.SetInteger(ANIM_PARAM_LEGSTATE, (int)m_CharacterController.characterMovementState);
    }
}