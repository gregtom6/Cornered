/// <summary>
/// Filename: CEquipmentVisualizer.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.Rendering.FilterWindow;

public class CEquipmentVisualizer : MonoBehaviour
{
    [SerializeField] private CProjectileVisualizer m_ProjectileVisualizer;
    [SerializeField] private EquipmentAndItsTransformsDict m_EquipmentTransforms;

    private Dictionary<EEquipment, List<GameObject>> m_VisualizedEquipmentElements = new();

    public void VisualizeEquipment(ItemDatas itemTypes)
    {
        if (m_VisualizedEquipmentElements.Count == 0)
        {
            InitializeDictionary();
        }

        TransformHolder transforms = GetUsedTransform(itemTypes.item, out EEquipment equipmentToBeReplaced);

        DestroyPreviouslyVisualizedElements(equipmentToBeReplaced);

        foreach (Transform transform in transforms.transforms)
        {
            GameObject prefab = AllConfig.Instance.EquipmentConfig.GetEquippedPrefab(itemTypes.item);
            if (prefab != null)
            {
                GameObject element = Instantiate(prefab, transform);
                m_VisualizedEquipmentElements[equipmentToBeReplaced].Add(element);
                SetupVisualizedEquipment(element);
            }
        }
    }

    private void SetupVisualizedEquipment(GameObject element)
    {
        CEquippedWeapon equippedWeapon = element.GetComponent<CEquippedWeapon>();

        if (equippedWeapon != null)
        {
            m_ProjectileVisualizer.SetOriginTransform(equippedWeapon.muzzleTransform);
        }
    }

    private void Start()
    {
        if (m_VisualizedEquipmentElements.Count == 0)
        {
            InitializeDictionary();
        }
    }

    private void InitializeDictionary()
    {
        for (int i = 0; i < (int)EEquipment.Count; i++)
        {
            m_VisualizedEquipmentElements.Add((EEquipment)i, new());
        }
    }

    private void DestroyPreviouslyVisualizedElements(EEquipment equipment)
    {
        m_VisualizedEquipmentElements[equipment].ForEach(x => Destroy(x));
        m_VisualizedEquipmentElements[equipment].Clear();
    }

    private TransformHolder GetUsedTransform(EItemType itemType, out EEquipment equipment)
    {
        equipment = AllConfig.Instance.EquipmentConfig.GetEquipmentTypeBasedOnItemType(itemType);

        return m_EquipmentTransforms[equipment];
    }
}

[Serializable]
public struct TransformHolder
{
    public List<Transform> transforms;
}

[System.Serializable]
public class EquipmentAndItsTransformsDict : SerializableDictionaryBase<EEquipment, TransformHolder> { }