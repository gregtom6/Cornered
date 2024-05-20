using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CProduct : CIngredient, IEquippable
{
    public override IEquippable GetEquippable()
    {
        return this;
    }

    public void Equip()
    {
        InventoryManager.instance.EquipItem(ECharacterType.Player, new ItemTypes(m_ItemType, m_ItemState));

        Destroy(gameObject);
    }
}
