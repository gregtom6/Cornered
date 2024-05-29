/// <summary>
/// Filename: GameEvents.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

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

public struct EquipmentDecidedEvent
{
    public ECharacterType characterType;
    public ItemDatas weaponItem;
    public ItemDatas shieldItem;
    public ItemDatas additionalItem;
}