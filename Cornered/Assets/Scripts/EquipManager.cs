using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoBehaviour
{
    public static EquipManager instance;

    private void Start()
    {
        instance = this;
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
        ActualizeEquipment(e.characterType, e.weaponItem);
        ActualizeEquipment(e.characterType, e.shieldItem);
        ActualizeEquipment(e.characterType, e.additionalItem);
    }

    public void Equip(ECharacterType characterType, ItemTypes itemTypes)
    {
        ActualizeEquipment(characterType, itemTypes);
    }

    private void ActualizeEquipment(ECharacterType characterType, ItemTypes itemTypes)
    {
        InventoryManager.instance.EquipItem(characterType, itemTypes);

        CEquipmentVisualizer equipmentVisualizer = CCharacterManager.instance.GetEquipmentVisualizer(characterType);

        if (equipmentVisualizer != null)
        {
            equipmentVisualizer.VisualizeEquipment(itemTypes);
        }
    }
}
