using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class StateMachine
{
    public StateMachineReferenceData stateMachineReferences;
    public State state => m_State;
    protected State m_State;

    public virtual void Initialize(State startingState)
    {
        m_State = startingState;
        m_State.Enter();
    }

    public void TransitionTo(State nextState)
    {
        m_State.Exit();
        m_State = nextState;
        m_State.Enter();
    }

    public void Update()
    {
        if (m_State == null)
        {
            return;
        }

        m_State.Update();
    }
}

public class StateMachineReferenceData
{ }

public class CharacterStateMachineReferenceData : StateMachineReferenceData
{
    public NavMeshAgent navMeshAgent;
    public CHealth health;
    public CWeapon weapon;
    public Transform characterTransform;
    public HideSpotFinder hideSpotFinder;
    public float maxHealth;

    public CharacterStateMachineReferenceData(NavMeshAgent navMeshAgent, CHealth health, CWeapon weapon, Transform characterTransform, HideSpotFinder hideSpotFinder, float maxHealth)
    {
        this.navMeshAgent = navMeshAgent;
        this.health = health;
        this.weapon = weapon;
        this.characterTransform = characterTransform;
        this.maxHealth = maxHealth;
        this.hideSpotFinder = hideSpotFinder;
    }
}