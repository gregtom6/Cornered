/// <summary>
/// Filename: CCharacterAnimator.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>
/// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CCharacterController))]
[RequireComponent(typeof(CWeapon))]
public class CCharacterAnimator : MonoBehaviour
{
    private readonly int ANIM_PARAM_LEGSTATE = Animator.StringToHash("legState");
    private readonly int ANIM_PARAM_ARMWEAPON = Animator.StringToHash("useWeapon");

    [SerializeField] private Animator m_Animator;
    private CCharacterController m_CharacterController;
    private CWeapon m_Weapon;

    private void Start()
    {
        m_CharacterController = GetComponent<CCharacterController>();
        m_Weapon = GetComponent<CWeapon>();
    }

    private void Update()
    {
        m_Animator.SetInteger(ANIM_PARAM_LEGSTATE, (int)m_CharacterController.characterMovementState);
        m_Animator.SetBool(ANIM_PARAM_ARMWEAPON, m_Weapon.IsThereEquippedWeapon());
    }
}