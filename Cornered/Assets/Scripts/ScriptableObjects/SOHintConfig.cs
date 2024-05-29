/// <summary>
/// Filename: SOHintConfig.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using RotaryHeart.Lib.SerializableDictionary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hint Config")]
public class SOHintConfig : ScriptableObject
{
    [SerializeField] private ItemHintMaterialDict m_ItemHintMaterialDict;

    public Material GetMaterialBasedOnItemType(EItemType itemType)
    {
        return m_ItemHintMaterialDict[itemType];
    }
}

[System.Serializable]
public class ItemHintMaterialDict : SerializableDictionaryBase<EItemType, Material> { }
