using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CEnemyController : MonoBehaviour
{
    private NavMeshAgent m_NavMeshAgent;

    private bool m_ShouldStartWalk;

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
    }

    private void OnTimeOverHappened(TimeOverHappenedEvent ev)
    {
        m_ShouldStartWalk = true;
    }

    private void Update()
    {
        if (!m_ShouldStartWalk)
        {
            return;
        }

        m_NavMeshAgent.destination = CCharacterManager.playerPosition;

        /*
        if (m_NavMeshAgent.remainingDistance <= m_NavMeshAgent.stoppingDistance)
        {
            m_NavMeshAgent.speed = 0f;
        }
        */
    }
}

public struct TimeOverHappenedEvent
{

}