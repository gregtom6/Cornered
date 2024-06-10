/// <summary>
/// Filename: CEnemyController.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CWeapon))]
public partial class CEnemyController : CCharacterController
{
    [SerializeField] private CHealth m_EnemyHealth;
    [SerializeField] private Transform m_HeadTransform;
    [SerializeField] private Transform m_MovementTargetPoint;
    [SerializeField] private LayerMask m_PlayerPillarLayerMask;
    [SerializeField] private LayerMask m_PillarLayerMask;

    private NavMeshAgent m_NavMeshAgent;
    private CharacterStateMachine m_StateMachine = new();
    private HideSpotFinder m_HideSpotFinder;

    private void Start()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        CWeapon m_AIWeapon = GetComponent<CWeapon>();
        m_NavMeshAgent.speed = AllConfig.Instance.CharacterConfig.enemyRunSpeed;
        m_StateMachine.Initialize(new WaitingState());
        m_HideSpotFinder = new HideSpotFinder(m_MovementTargetPoint,transform, m_PlayerPillarLayerMask, m_PillarLayerMask);

        m_StateMachine.stateMachineReferences =
            new CharacterStateMachineReferenceData(m_NavMeshAgent, m_EnemyHealth, m_AIWeapon, transform, m_HideSpotFinder, AllConfig.Instance.CharacterConfig.enemyMaxHealth);
    }

    private void OnDisable()
    {
        m_StateMachine.Unsubscribe();
    }

    private void Update()
    {
        if (m_StateMachine.state is WaitingState)
        {
            return;
        }

        m_HeadTransform.LookAt(CCharacterManager.instance.GetCharacterPosition(ECharacterType.Player));

        m_StateMachine.Update();

        m_MovementState = m_NavMeshAgent.velocity.magnitude <= 1f ? EMovementState.Standing : EMovementState.Walking;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(m_MovementTargetPoint.position, Vector3.one * 1f);
    }

    [ContextMenu("Kill Enemy")]
    private void DebugKillEnemy()
    {
        m_EnemyHealth.DamageHealth(float.MinValue);
    }
}