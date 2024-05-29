using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWeaponProduct : CProduct, IEquippable
{
    public override void Equip()
    {
        EquipManager.instance.Equip(ECharacterType.Player, new WeaponItemDatas(m_ItemType, m_ItemState));

        base.Equip();
    }
}
