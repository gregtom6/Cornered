using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyHealth : CHealth
{
    public override float GetMaxHealth()
    {
        return AllConfig.Instance.CharacterConfig.enemyMaxHealth;
    }
    protected override ECharacterType GetCharacterType()
    {
        return ECharacterType.Enemy;
    }

    protected override float GetReloadWaitingMaxTime()
    {
        return AllConfig.Instance.CharacterConfig.enemyWaitUntilHealthReloadStarts;
    }
}
