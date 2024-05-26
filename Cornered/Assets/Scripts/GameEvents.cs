using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CharacterDefeatedEvent
{
    public ECharacterType characterType;
}

public struct NewMatchStartedEvent
{
    public int matchIndex;
}

public struct TimeOverHappenedEvent { }

public struct EnemyGeneratedEvent
{
    public GameObject enemy;
}

public struct CharacterInitializedEvent
{
    public ECharacterType characterType;
    public CHealth healthComponent;
}