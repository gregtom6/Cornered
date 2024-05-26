using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEquipmentVisualizer : MonoBehaviour
{
    [SerializeField] private Transform m_ShieldEquipmentParent;
    [SerializeField] private Transform m_WeaponEquipmentParent;
    [SerializeField] private Transform m_AdditionalLeftEquipmentParent;
    [SerializeField] private Transform m_AdditionalRightEquipmentParent;

    private Dictionary<EEquipment, List<GameObject>> m_VisualizedEquipmentElements = new();

    public void VisualizeEquipment(ItemTypes itemTypes)
    {
        if (m_VisualizedEquipmentElements.Count == 0)
        {
            InitializeDictionary();
        }

        List<Transform> transforms = GetUsedTransform(itemTypes.item, out EEquipment equipmentToBeReplaced);

        DestroyPreviouslyVisualizedElements(equipmentToBeReplaced);

        foreach (Transform transform in transforms)
        {
            GameObject prefab = AllConfig.Instance.WeaponConfig.GetEquippedPrefab(itemTypes.item);
            if (prefab != null)
            {
                GameObject element = Instantiate(prefab, transform);
                m_VisualizedEquipmentElements[equipmentToBeReplaced].Add(element);
            }
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

    private List<Transform> GetUsedTransform(EItemType itemType, out EEquipment equipment)
    {
        List<Transform> transforms = new();
        equipment = EEquipment.Count;

        if (AllConfig.Instance.WeaponConfig.IsWeapon(itemType))
        {
            equipment = EEquipment.Weapon;
            transforms.Add(m_WeaponEquipmentParent);
        }
        else if (AllConfig.Instance.WeaponConfig.IsShield(itemType))
        {
            equipment = EEquipment.Shield;
            transforms.Add(m_ShieldEquipmentParent);
        }
        else if (AllConfig.Instance.WeaponConfig.IsAdditional(itemType))
        {
            equipment = EEquipment.Additional;
            transforms.Add(m_AdditionalLeftEquipmentParent);
            transforms.Add(m_AdditionalRightEquipmentParent);
        }

        return transforms;
    }
}

public enum EEquipment
{
    Weapon,
    Shield,
    Additional,

    Count,
}