using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerCharacterAnimator : CCharacterAnimator
{
    protected override void Update()
    {
        base.Update();

        m_Animator.SetBool(ANIM_PARAM_ARMWEAPON, InventoryManager.instance.currentPlayerWeapon.item != EItemType.Count);
    }
}
