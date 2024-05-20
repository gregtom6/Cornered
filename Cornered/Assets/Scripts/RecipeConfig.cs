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

    public EItemType GetResultItem(List<ItemTypes> itemTypes)
    {
        KeyValuePair<EItemType, ItemTypeDetails> item = m_RecipeDict.Where(x => AreListsEqual(itemTypes, x.Value.items)).FirstOrDefault();
        return item.Key;
    }

    public bool AreAnyRecipesContainThese(List<ItemTypes> items)
    {
        return m_RecipeDict.Any(x =>
        {
            return items.TrueForAll(y => x.Value.items.Contains(y));
        });
    }

    private bool AreListsEqual(IReadOnlyList<ItemTypes> first, IReadOnlyList<ItemTypes> second)
    {
        if (first == null || second == null)
            return false;

        if (first.Count != second.Count)
            return false;

        var dict1 = GetElementCounts(first);
        var dict2 = GetElementCounts(second);

        foreach (var kvp in dict1)
        {
            if (!dict2.ContainsKey(kvp.Key) || dict2[kvp.Key] != kvp.Value)
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
public class ItemTypes
{
    public EItemType item = EItemType.Count;
    public EItemState state = EItemState.Count;

    public ItemTypes(EItemType itemType, EItemState itemState)
    {
        item = itemType;
        state = itemState;
    }
}

[System.Serializable]
public class RecipeDict : SerializableDictionaryBase<EItemType, ItemTypeDetails> { }

public enum EItemState
{
    Normal,
    Freezed,
    Burned,

    Count,
}