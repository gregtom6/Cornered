using JetBrains.Annotations;
using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Config")]
public class WeaponConfig : ScriptableObject
{
    [SerializeField] private List<ItemTypes> m_Weapons = new();
    [SerializeField] private List<ItemTypes> m_Shields = new();
    [SerializeField] private List<ItemTypes> m_Additionals = new();

    [SerializeField] private WeaponSettingsDict m_WeaponSettingsDict;
    [SerializeField] private ShieldSettingsDict m_ShieldSettingsDict;
    [SerializeField] private AdditionalSettingsDict m_AdditionalSettingsDict;

    public ItemTypes GetRandomWeapon()
    {
        return m_Weapons.GetRandom();
    }

    public ItemTypes GetRandomShield()
    {
        return m_Shields.GetRandom();
    }

    public ItemTypes GetRandomAdditional()
    {
        return m_Additionals.GetRandom();
    }

    public bool IsWeapon(EItemType eItemType)
    {
        return m_Weapons.Any(x => x.item == eItemType);
    }

    public bool IsShield(EItemType eItemType)
    {
        return m_Shields.Any(x => x.item == eItemType);
    }

    public bool IsAdditional(EItemType eItemType)
    {
        return m_Additionals.Any(x => x.item == eItemType);
    }

    public WeaponSettings GetWeaponSettings(EItemType itemType)
    {
        return m_WeaponSettingsDict[itemType];
    }

    public ShieldSettings GetShieldSettings(EItemType itemType)
    {
        return m_ShieldSettingsDict[itemType];
    }

    public AdditionalSettings GetAdditionalSettings(EItemType itemType)
    {
        return m_AdditionalSettingsDict[itemType];
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