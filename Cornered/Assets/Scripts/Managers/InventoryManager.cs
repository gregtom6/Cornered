using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public ItemDatas currentPlayerWeapon => m_CharacterInventories[ECharacterType.Player].weapon;

    public ItemDatas currentPlayerAdditional => m_CharacterInventories[ECharacterType.Player].additional;

    public ItemDatas currentEnemyWeapon => m_CharacterInventories[ECharacterType.Enemy].weapon;

    public ItemDatas currentEnemyShield => m_CharacterInventories[ECharacterType.Enemy].shield;

    private Dictionary<ECharacterType, CurrentInventory> m_CharacterInventories = new();

    public static InventoryManager instance;

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
}