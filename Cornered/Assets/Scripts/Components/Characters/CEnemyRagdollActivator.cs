/// <summary>
/// Filename: CEnemyRagdollActivator.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CEnemyRagdollActivator : CRagdollActivator
{
    private NavMeshAgent m_NavmeshAgent;

    protected override void Start()
    {
        base.Start();
        m_NavmeshAgent = GetComponent<NavMeshAgent>();
    }

    protected override void OnCharacterDefeated(CharacterDefeatedEvent ev)
    {
        base.OnCharacterDefeated(ev);

        if (ev.characterType==ECharacterType.Player)
        {
            return;
        }

        m_NavmeshAgent.enabled = false;
    }
}
