using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DefenseState : State
{
    private StateMachine m_StateMachine;
    private CharacterStateMachineReferenceData m_CharacterStateMachineReferences;

    public DefenseState(StateMachine stateMachine, CharacterStateMachineReferenceData characterStateMachineReferences)
    {
        m_StateMachine = stateMachine;
        m_CharacterStateMachineReferences = characterStateMachineReferences;
    }

    public override void Update()
    {
        Vector3? position = m_CharacterStateMachineReferences.hideSpotFinder.GetClosestHidingSpot();
        m_CharacterStateMachineReferences.navMeshAgent.destination = position.HasValue ? position.Value : m_CharacterStateMachineReferences.characterTransform.position;

        if ((m_CharacterStateMachineReferences.health.currentHealth / m_CharacterStateMachineReferences.maxHealth) * 100f >= AllConfig.Instance.AIConfig.attackWhenLifeMoreThanPercentage && m_CharacterStateMachineReferences.weapon.isReadyToShoot)
        {
            m_StateMachine.TransitionTo(new AttackState(m_CharacterStateMachineReferences, m_StateMachine));
        }
    }
}
