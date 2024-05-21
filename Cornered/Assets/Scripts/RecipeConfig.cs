using JetBrains.Annotations;
using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe Config")]
public class RecipeConfig : ScriptableObject
{
    [SerializeField] private RecipeDict m_RecipeDict;
    [SerializeField] private ProductPrefabDict m_ProductPrefabDict;

    public GameObject GetResultItem(IReadOnlyList<ItemTypes> itemTypes)
    {
        KeyValuePair<EItemType, ItemTypeDetails> item = m_RecipeDict.Where(x => AreListsEqual(itemTypes, x.Value.items)).FirstOrDefault();
        EItemType resultItemType = item.Key;
        return m_ProductPrefabDict[resultItemType];
    }

    public bool AreAnyRecipesContainThese(List<ItemTypes> items)
    {
        return m_RecipeDict.Any(x =>
        {
            return items.TrueForAll(y => x.Value.items.Contains(y));
        });
    }

    public static bool AreListsEqual(IReadOnlyList<ItemTypes> list1, IReadOnlyList<ItemTypes> list2)
    {
        if (list1 == null || list2 == null)
            return list1 == list2;

        if (list1.Count != list2.Count)
            return false;

        for (int i = 0; i < list1.Count; i++)
        {
            if (!list1[i].Equals(list2[i]))
                return false;
        }

        return true;
    }

    private Dictionary<ItemTypes, int> GetElementCounts(IReadOnlyList<ItemTypes> list)
    {
        var dict = new Dictionary<ItemTypes, int>();

        foreach (var element in list)
        {
            if (dict.ContainsKey(element))
                dict[element]++;
            else
                dict[element] = 1;
        }

        return dict;
    }
}

[Serializable]
public class ItemTypeDetails
{
    public List<ItemTypes> items = new();
}

[Serializable]
public struct ItemTypes
{
    public EItemType item;
    public EItemState state;

    public ItemTypes(EItemType itemType, EItemState itemState)
    {
        item = itemType;
        state = itemState;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        ItemTypes other = (ItemTypes)obj;
        return item == other.item && state == other.state;
    }

    public override int GetHashCode()
    {
        int hashItem = item.GetHashCode();
        int hashState = state.GetHashCode();
        return hashItem ^ hashState;
    }
}

[System.Serializable]
public class RecipeDict : SerializableDictionaryBase<EItemType, ItemTypeDetails> { }

[System.Serializable]
public class ProductPrefabDict : SerializableDictionaryBase<EItemType, GameObject> { }

public enum EItemState
{
    Normal,
    Freezed,
    Burned,

    Count,
}