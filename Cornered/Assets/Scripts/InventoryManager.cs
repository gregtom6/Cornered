using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public ItemTypes currentPlayerWeapon => m_CharacterInventories[ECharacterType.Player].weapon;

    public ItemTypes currentPlayerAdditional => m_CharacterInventories[ECharacterType.Player].additional;

    public ItemTypes currentEnemyWeapon => m_CharacterInventories[ECharacterType.Enemy].weapon;

    private Dictionary<ECharacterType, CurrentInventory> m_CharacterInventories = new();

    public static InventoryManager instance;

    public void EquipItem(ECharacterType type, ItemTypes itemTypes)
    {
        if (AllConfig.Instance.WeaponConfig.IsWeapon(itemTypes.item))
        {
            EquipWeaponType(type, itemTypes);
        }
        else if (AllConfig.Instance.WeaponConfig.IsShield(itemTypes.item))
        {
            EquipShieldType(type, itemTypes);
        }
        else if (AllConfig.Instance.WeaponConfig.IsAdditional(itemTypes.item))
        {
            EquipAdditionalType(type, itemTypes);
        }
    }

    private void EquipWeaponType(ECharacterType characterType, ItemTypes itemTypes)
    {
        CurrentInventory currentInventory = m_CharacterInventories[characterType];
        currentInventory.weapon = itemTypes;
        m_CharacterInventories[characterType] = currentInventory;
    }

    private void EquipShieldType(ECharacterType characterType, ItemTypes itemTypes)
    {
        CurrentInventory currentInventory = m_CharacterInventories[characterType];
        currentInventory.shield = itemTypes;
        m_CharacterInventories[characterType] = currentInventory;
    }

    private void EquipAdditionalType(ECharacterType characterType, ItemTypes itemTypes)
    {
        CurrentInventory currentInventory = m_CharacterInventories[characterType];
        currentInventory.additional = itemTypes;
        m_CharacterInventories[characterType] = currentInventory;
    }

    private void Awake()
    {
        instance = this;

        ItemTypes emptyItemTypes = new ItemTypes(EItemType.Count, EItemState.Count);

        m_CharacterInventories.Add(ECharacterType.Player, new CurrentInventory(emptyItemTypes, emptyItemTypes, emptyItemTypes));
        m_CharacterInventories.Add(ECharacterType.Enemy, new CurrentInventory(emptyItemTypes, emptyItemTypes, emptyItemTypes));
    }

    private void OnEnable()
    {
        EventManager.AddListener<EquipmentDecidedEvent>(OnEquipmentDecided);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<EquipmentDecidedEvent>(OnEquipmentDecided);
    }

    private void OnEquipmentDecided(EquipmentDecidedEvent e)
    {
        CurrentInventory currentInventory = m_CharacterInventories[e.characterType];
        currentInventory.weapon = e.weaponItem;
        currentInventory.shield = e.shieldItem;
        currentInventory.additional = e.additionalItem;

        m_CharacterInventories[ECharacterType.Enemy] = currentInventory;
    }
}

public struct CurrentInventory
{
    public ItemTypes weapon;
    public ItemTypes shield;
    public ItemTypes additional;

    public CurrentInventory(ItemTypes weapon, ItemTypes shield, ItemTypes additional)
    {
        this.weapon = weapon;
        this.shield = shield;
        this.additional = additional;
    }
}