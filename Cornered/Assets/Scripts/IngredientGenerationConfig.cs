using JetBrains.Annotations;
using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient Generation Config")]
public class IngredientGenerationConfig : ScriptableObject
{
    [SerializeField] private IngredientGenerationDict m_IngredientGenerationDict;

    public CIngredient GetWeightedRandomItemPrefab()
    {
        int m_SumOfWeights = 0;

        foreach (KeyValuePair<EItemType, ItemGenerationDatas> item in m_IngredientGenerationDict)
        {
            m_SumOfWeights += item.Value.weightForAppear;
        }

        int random = UnityEngine.Random.Range(0, m_SumOfWeights);

        m_SumOfWeights = 0;

        foreach (KeyValuePair<EItemType, ItemGenerationDatas> item in m_IngredientGenerationDict)
        {
            m_SumOfWeights += item.Value.weightForAppear;
            if (m_SumOfWeights >= random)
            {
                return item.Value.itemPrefab;
            }
        }

        return null;
    }
}

[Serializable]
public struct ItemGenerationDatas
{
    public int weightForAppear;
    public CIngredient itemPrefab;
}

[System.Serializable]
public class IngredientGenerationDict : SerializableDictionaryBase<EItemType, ItemGenerationDatas> { }

public enum EItemType
{
    EmptyItem,
    Tube,
    Marbles,
    Board,
    Coffee,
    Boots,
    Petrol,
    Pistol,
    Shotgun,
    FastBoots,
    FlamingShotgun,
    Freezer,
    DefenderPhysical,
    DefenderPhysicalExtra,
    DefenderHeat,
    DefenderCold,
    Rope,

    Count,
}