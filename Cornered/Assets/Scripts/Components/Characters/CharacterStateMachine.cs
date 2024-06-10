using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterStateMachine : StateMachine
{
    public override void Initialize(State startingState)
    {
        base.Initialize(startingState);
        EventManager.AddListener<TimeOverHappenedEvent>(OnTimeOverHappened);
    }

    public void Unsubscribe() 
    {
        EventManager.RemoveListener<TimeOverHappenedEvent>(OnTimeOverHappened);
    }

    private void OnTimeOverHappened(TimeOverHappenedEvent ev)
    {
        TransitionTo(new AttackState(stateMachineReferences as CharacterStateMachineReferenceData, this));
    }
}