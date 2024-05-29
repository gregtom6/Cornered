/// <summary>
/// Filename: CPlayerHealth.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerHealth : CHealth
{
    public override float GetMaxHealth()
    {
        return AllConfig.Instance.CharacterConfig.maxHealth;
    }
    protected override ECharacterType GetCharacterType()
    {
        return ECharacterType.Player;
    }

    protected override float GetReloadWaitingMaxTime()
    {
        return AllConfig.Instance.CharacterConfig.waitUntilHealthReloadStarts;
    }
}
