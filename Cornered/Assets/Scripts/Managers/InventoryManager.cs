/// <summary>
/// Filename: InventoryManager.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    private Dictionary<ECharacterType, CurrentInventory> m_CharacterInventories = new();
    public CurrentInventory GetCopyOfCurrentInventory(ECharacterType characterType)
    {
        return characterType == ECharacterType.Player ? m_CharacterInventories[ECharacterType.Player].Copy() : m_CharacterInventories[ECharacterType.Enemy].Copy();
    }

    public void EquipItem(ECharacterType type, ItemDatas itemTypes)
    {
        CurrentInventory currentInventory = m_CharacterInventories[type];

        itemTypes.Equip(currentInventory);

        m_CharacterInventories[type] = currentInventory;
    }

    private void Awake()
    {
        instance = this;

        ItemDatas emptyItemTypes = new ItemDatas(EItemType.Count, EItemState.Count);

        m_CharacterInventories.Add(ECharacterType.Player, new CurrentInventory(emptyItemTypes, emptyItemTypes, emptyItemTypes));
        m_CharacterInventories.Add(ECharacterType.Enemy, new CurrentInventory(emptyItemTypes, emptyItemTypes, emptyItemTypes));
    }
}

public class CurrentInventory
{
    public ItemDatas weapon;
    public ItemDatas shield;
    public ItemDatas additional;

    public CurrentInventory(ItemDatas weapon, ItemDatas shield, ItemDatas additional)
    {
        this.weapon = weapon;
        this.shield = shield;
        this.additional = additional;
    }

    public CurrentInventory Copy()
    {
        return new CurrentInventory(weapon, shield, additional);
    }
}