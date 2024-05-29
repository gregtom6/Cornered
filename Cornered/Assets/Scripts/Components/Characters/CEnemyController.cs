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
public partial class CEnemyController : CCharacterController
{
    [SerializeField] private CHealth m_EnemyHealth;
    [SerializeField] private Transform m_HeadTransform;
    [SerializeField] private Transform m_MovementTargetPoint;
    [SerializeField] private LayerMask m_PlayerPillarLayerMask;
    [SerializeField] private LayerMask m_PillarLayerMask;

    private CWeapon m_AIWeapon;

    private NavMeshAgent m_NavMeshAgent;

    private EEnemyState state = EEnemyState.Waiting;

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
        state = EEnemyState.ShootPosition;
    }

    private void Update()
    {
        if (state == EEnemyState.Waiting)
        {
            return;
        }

        m_HeadTransform.LookAt(CCharacterManager.instance.playerPosition);

        if (state == EEnemyState.DefendPosition)
        {
            HideSpotFinder hideSpotFinder = new HideSpotFinder(m_MovementTargetPoint, transform, m_PlayerPillarLayerMask, m_PillarLayerMask);
            Vector3? position = hideSpotFinder.GetClosestHidingSpot();
            m_NavMeshAgent.destination = position.HasValue ? position.Value : transform.position;
        }
        else if (state == EEnemyState.ShootPosition)
        {
            m_NavMeshAgent.destination = CCharacterManager.instance.playerPosition;
        }


        if (state == EEnemyState.DefendPosition)
        {
            if ((m_EnemyHealth.currentHealth / AllConfig.Instance.CharacterConfig.enemyMaxHealth) * 100f >= AllConfig.Instance.AIConfig.attackWhenLifeMoreThanPercentage && m_AIWeapon.isReadyToShoot)
            {
                state = EEnemyState.ShootPosition;
            }
        }
        else if (state == EEnemyState.ShootPosition)
        {
            if (!m_AIWeapon.isReadyToShoot || (m_EnemyHealth.currentHealth / AllConfig.Instance.CharacterConfig.enemyMaxHealth) * 100f <= AllConfig.Instance.AIConfig.hideWhenLifeLessThanPercentage)
            {
                state = EEnemyState.DefendPosition;
            }
        }

        movementState = m_NavMeshAgent.velocity.magnitude <= 1f ? EMovementState.Standing : EMovementState.Walking;
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