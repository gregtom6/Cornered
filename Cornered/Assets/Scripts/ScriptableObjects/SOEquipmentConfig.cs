/// <summary>
/// Filename: SOEquipmentConfig.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using JetBrains.Annotations;
using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment Config")]
public class SOEquipmentConfig : ScriptableObject
{
    [SerializeField] private List<WeaponItemDatas> m_Weapons = new();
    [SerializeField] private List<ShieldItemDatas> m_Shields = new();
    [SerializeField] private List<AdditionalItemDatas> m_Additionals = new();

    [SerializeField] private WeaponSettingsDict m_WeaponSettingsDict;
    [SerializeField] private ShieldSettingsDict m_ShieldSettingsDict;
    [SerializeField] private AdditionalSettingsDict m_AdditionalSettingsDict;
    [SerializeField] private EquippedPrefabDict m_EquippedPrefabDict;

    public ItemDatas GetRandomWeapon() => m_Weapons.GetRandom();

    public ItemDatas GetRandomShield() => m_Shields.GetRandom();

    public ItemDatas GetRandomAdditional() => m_Additionals.GetRandom();

    public bool IsWeapon(EItemType eItemType) => m_Weapons.Any(x => x.item == eItemType);

    public bool IsShield(EItemType eItemType) => m_Shields.Any(x => x.item == eItemType);

    public bool IsAdditional(EItemType eItemType) => m_Additionals.Any(x => x.item == eItemType);

    public WeaponSettings GetWeaponSettings(EItemType itemType) => m_WeaponSettingsDict[itemType];

    public ShieldSettings GetShieldSettings(EItemType itemType) => m_ShieldSettingsDict[itemType];

    public AdditionalSettings GetAdditionalSettings(EItemType itemType)  => m_AdditionalSettingsDict[itemType];

    public GameObject GetEquippedPrefab(EItemType itemType)
    {
        if (m_EquippedPrefabDict.ContainsKey(itemType))
        {
            return m_EquippedPrefabDict[itemType];
        }

        return null;
    }

    public EEquipment GetEquipmentTypeBasedOnItemType(EItemType itemType)
    {
        if (AllConfig.Instance.EquipmentConfig.IsWeapon(itemType))
        {
            return EEquipment.Weapon;
        }
        else if (AllConfig.Instance.EquipmentConfig.IsShield(itemType))
        {
            return EEquipment.Shield;
        }
        else if (AllConfig.Instance.EquipmentConfig.IsAdditional(itemType))
        {
            return EEquipment.Additional;
        }

        return EEquipment.Count;
    }
}

[Serializable]
public struct WeaponSettings
{
    public float cooldownTimeInSec;
    public float damage;
}

[Serializable]
public struct ShieldSettings
{
    public float damageDivider;
}

[Serializable]
public struct AdditionalSettings
{
    public float multiplier;
}

[System.Serializable]
public class WeaponSettingsDict : SerializableDictionaryBase<EItemType, WeaponSettings> { }

[System.Serializable]
public class ShieldSettingsDict : SerializableDictionaryBase<EItemType, ShieldSettings> { }

[Serializable]
public class AdditionalSettingsDict : SerializableDictionaryBase<EItemType, AdditionalSettings> { }

[Serializable]
public class EquippedPrefabDict : SerializableDictionaryBase<EItemType, GameObject> { }