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

    private int m_SumOfWeights;

    public GameObject GetWeightedRandomItemPrefab()
    {
        int random = UnityEngine.Random.Range(0, m_SumOfWeights);

        int sum = 0;

        foreach (KeyValuePair<EItemType, ItemGenerationDatas> item in m_IngredientGenerationDict)
        {
            sum += item.Value.weightForAppear;
            if (sum >= random)
            {
                return item.Value.itemPrefab;
            }
        }

        return null;
    }

    private void Awake()
    {
        foreach (KeyValuePair<EItemType, ItemGenerationDatas> item in m_IngredientGenerationDict)
        {
            m_SumOfWeights += item.Value.weightForAppear;
        }
    }
}

[Serializable]
public struct ItemGenerationDatas
{
    public int weightForAppear;
    public GameObject itemPrefab;
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