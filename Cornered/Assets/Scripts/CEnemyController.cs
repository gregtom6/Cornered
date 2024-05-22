using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CEnemyController : MonoBehaviour
{
    [SerializeField] private CHealth m_EnemyHealth;
    [SerializeField] private Transform m_HeadTransform;

    private CAIWeapon m_AIWeapon;

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
        m_AIWeapon = GetComponent<CAIWeapon>();
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

        m_HeadTransform.LookAt(CCharacterManager.playerPosition);

        if (state == EEnemyState.DefendPosition)
        {
            m_NavMeshAgent.destination = GetClosestHidingSpot();
        }
        else if (state == EEnemyState.ShootPosition)
        {
            m_NavMeshAgent.destination = GetShootPosition();
        }


        if (state == EEnemyState.DefendPosition)
        {
            if ((m_EnemyHealth.currentHealth / AllConfig.Instance.CharacterConfig.maxHealth) * 100f >= AllConfig.Instance.AIConfig.attackWhenLifeMoreThanPercentage && m_AIWeapon.isReadyToShoot)
            {
                state = EEnemyState.ShootPosition;
            }
        }
        else if (state == EEnemyState.ShootPosition)
        {
            if (!m_AIWeapon.isReadyToShoot || (m_EnemyHealth.currentHealth / AllConfig.Instance.CharacterConfig.maxHealth) * 100f <= AllConfig.Instance.AIConfig.hideWhenLifeLessThanPercentage)
            {
                state = EEnemyState.DefendPosition;
            }
        }
    }

    Vector3 GetShootPosition()
    {
        return CCharacterManager.playerPosition;
    }

    Vector3 GetClosestHidingSpot()
    {
        return Vector3.zero;
    }
}

public struct TimeOverHappenedEvent
{

}

public enum EEnemyState
{
    Waiting,
    ShootPosition,
    DefendPosition,

    Count,
}