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

    private CWeapon m_AIWeapon;
    private NavMeshAgent m_NavMeshAgent;
    private EEnemyState m_State = EEnemyState.Waiting;

    private void OnEnable()
    {
        EventManager.AddListener<TimeOverHappenedEvent>(OnTimeOverHappened);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<TimeOverHappenedEvent>(OnTimeOverHappened);
    }

    private void Start()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_AIWeapon = GetComponent<CWeapon>();
        m_NavMeshAgent.speed = AllConfig.Instance.CharacterConfig.enemyRunSpeed;
    }

    private void OnTimeOverHappened(TimeOverHappenedEvent ev)
    {
        m_State = EEnemyState.ShootPosition;
    }

    private void Update()
    {
        if (m_State == EEnemyState.Waiting)
        {
            return;
        }

        m_HeadTransform.LookAt(CCharacterManager.instance.playerPosition);

        ProcessCurrentState();
        SwitchStateIfNeeded();

        m_MovementState = m_NavMeshAgent.velocity.magnitude <= 1f ? EMovementState.Standing : EMovementState.Walking;
    }

    private void ProcessCurrentState()
    {
        if (m_State == EEnemyState.DefendPosition)
        {
            HideSpotFinder hideSpotFinder = new HideSpotFinder(m_MovementTargetPoint, transform, m_PlayerPillarLayerMask, m_PillarLayerMask);
            Vector3? position = hideSpotFinder.GetClosestHidingSpot();
            m_NavMeshAgent.destination = position.HasValue ? position.Value : transform.position;
        }
        else if (m_State == EEnemyState.ShootPosition)
        {
            m_NavMeshAgent.destination = CCharacterManager.instance.playerPosition;
        }
    }

    private void SwitchStateIfNeeded()
    {
        if (m_State == EEnemyState.DefendPosition)
        {
            if ((m_EnemyHealth.currentHealth / AllConfig.Instance.CharacterConfig.enemyMaxHealth) * 100f >= AllConfig.Instance.AIConfig.attackWhenLifeMoreThanPercentage && m_AIWeapon.isReadyToShoot)
            {
                m_State = EEnemyState.ShootPosition;
            }
        }
        else if (m_State == EEnemyState.ShootPosition)
        {
            if (!m_AIWeapon.isReadyToShoot || (m_EnemyHealth.currentHealth / AllConfig.Instance.CharacterConfig.enemyMaxHealth) * 100f <= AllConfig.Instance.AIConfig.hideWhenLifeLessThanPercentage)
            {
                m_State = EEnemyState.DefendPosition;
            }
        }
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