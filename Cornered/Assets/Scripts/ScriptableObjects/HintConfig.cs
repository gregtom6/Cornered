using RotaryHeart.Lib.SerializableDictionary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hint Config")]
public class HintConfig : ScriptableObject
{
    [SerializeField] private ItemHintMaterialDict m_ItemHintMaterialDict;

    public Material GetMaterialBasedOnItemType(EItemType itemType)
    {
        return m_ItemHintMaterialDict[itemType];
    }
}

[System.Serializable]
public class ItemHintMaterialDict : SerializableDictionaryBase<EItemType, Material> { }
