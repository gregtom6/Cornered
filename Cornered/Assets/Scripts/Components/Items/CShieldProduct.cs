/// <summary>
/// Filename: CShieldProduct.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CShieldProduct : CProduct, IEquippable
{
    public override void Equip()
    {
        EquipManager.instance.Equip(ECharacterType.Player, new ShieldItemDatas(m_ItemType, m_ItemState));

        base.Equip();
    }
}
