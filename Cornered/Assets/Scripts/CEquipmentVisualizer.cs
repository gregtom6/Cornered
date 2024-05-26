using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEquipmentVisualizer : MonoBehaviour
{
    [SerializeField] private Transform m_ShieldEquipmentParent;
    [SerializeField] private Transform m_WeaponEquipmentParent;

    public void EquipFromInventory()
    {
        ItemTypes weapon = InventoryManager.instance.currentEnemyWeapon;
        ItemTypes shield = InventoryManager.instance.currentEnemyShield;

        //Instantiate(AllConfig.Instance.WeaponConfig.GetEquippedPrefab(weapon.item), m_WeaponEquipmentParent);
        Instantiate(AllConfig.Instance.WeaponConfig.GetEquippedPrefab(shield.item), m_ShieldEquipmentParent);
    }
}
