using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : State
{
    private StateMachine m_StateMachine;
    private CharacterStateMachineReferenceData m_CharacterStateMachineReferences;

    public AttackState(CharacterStateMachineReferenceData characterStateMachineReferences, StateMachine stateMachine)
    {
        m_CharacterStateMachineReferences = characterStateMachineReferences;
        m_StateMachine = stateMachine;
    }

    public override void Update()
    {
        m_CharacterStateMachineReferences.navMeshAgent.destination = CCharacterManager.instance.GetCharacterPosition(ECharacterType.Player);

        if (!m_CharacterStateMachineReferences.weapon.isReadyToShoot || (m_CharacterStateMachineReferences.health.currentHealth / m_CharacterStateMachineReferences.maxHealth) * 100f <= AllConfig.Instance.AIConfig.hideWhenLifeLessThanPercentage)
        {
            m_StateMachine.TransitionTo(new DefenseState(m_StateMachine, m_CharacterStateMachineReferences));
        }
    }
}
