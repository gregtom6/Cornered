using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyCharacterAnimator : CCharacterAnimator
{
    protected override void Update()
    {
        base.Update();

        m_Animator.SetBool(ANIM_PARAM_ARMWEAPON, InventoryManager.instance.currentEnemyWeapon.item != EItemType.Count);
    }
}
