/// <summary>
/// Filename: CWeaponHint.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using RotaryHeart.Lib.SerializableDictionary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEquipmentHint : MonoBehaviour
{
    [SerializeField] private EquipmentAndItsMeshRendererDict m_EquipmentAndItsMeshRendererDict;

    private void OnEnable()
    {
        EventManager.AddListener<EquipmentDecidedEvent>(OnEnemyEquipmentDecidedEvent);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<EquipmentDecidedEvent>(OnEnemyEquipmentDecidedEvent);
    }

    private void OnEnemyEquipmentDecidedEvent(EquipmentDecidedEvent e)
    {
        m_EquipmentAndItsMeshRendererDict[EEquipment.Weapon].material = AllConfig.Instance.HintConfig.GetMaterialBasedOnItemType(e.weaponItem.item);
        m_EquipmentAndItsMeshRendererDict[EEquipment.Shield].material = AllConfig.Instance.HintConfig.GetMaterialBasedOnItemType(e.shieldItem.item);
    }
}

[System.Serializable]
public class EquipmentAndItsMeshRendererDict : SerializableDictionaryBase<EEquipment, MeshRenderer> { }