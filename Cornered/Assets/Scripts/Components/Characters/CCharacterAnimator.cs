using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CCharacterAnimator : MonoBehaviour
{
    protected readonly int ANIM_PARAM_LEGSTATE = Animator.StringToHash("legState");
    protected readonly int ANIM_PARAM_ARMWEAPON = Animator.StringToHash("useWeapon");

    [SerializeField] protected Animator m_Animator;
    protected CCharacterController m_CharacterController;

    protected void Start()
    {
        m_CharacterController = GetComponent<CCharacterController>();
    }

    protected virtual void Update()
    {
        m_Animator.SetInteger(ANIM_PARAM_LEGSTATE, (int)m_CharacterController.characterMovementState);
    }
}