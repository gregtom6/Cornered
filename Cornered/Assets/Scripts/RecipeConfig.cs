using JetBrains.Annotations;
using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.FullSerializer.Internal.Converters;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe Config")]
public class RecipeConfig : ScriptableObject
{
    [SerializeField] private Material m_RecipeShowPlusMaterial;
    [SerializeField] private Material m_RecipeShowEqualMaterial;
    [SerializeField] private GameObject m_RecipeShowElementPrefab;
    [SerializeField] private RecipeDict m_RecipeDict;
    [SerializeField] private ProductPrefabDict m_ProductPrefabDict;
    [SerializeField] private IngredientRadiatingMaterialDict m_IngredientRadiatingMaterialDict;

    public Material plusSignMaterial => m_RecipeShowPlusMaterial;
    public Material equalSignMaterial => m_RecipeShowEqualMaterial;

    public GameObject recipeShowElementPrefab => m_RecipeShowElementPrefab;

    public GameObject GetResultItem(IReadOnlyList<ItemTypes> itemTypes)
    {
        KeyValuePair<EItemType, ItemTypeDetails> item = m_RecipeDict.Where(x => AreListsEqual(itemTypes, x.Value.items)).FirstOrDefault();
        EItemType resultItemType = item.Key;
        return m_ProductPrefabDict[resultItemType];
    }

    public IReadOnlyList<IReadOnlyList<Material>> GetRadiatingMaterialsOfAllRecipes()
    {
        List<List<Material>> materials = new();

        int i = 0;

        foreach (KeyValuePair<EItemType, ItemTypeDetails> recipe in m_RecipeDict)
        {
            materials.Add(new());

            foreach (ItemTypes itemTypes in recipe.Value.items)
            {
                Material mat = m_IngredientRadiatingMaterialDict[itemTypes.item];

                materials[i].Add(mat);
            }

            Material endMat = m_IngredientRadiatingMaterialDict[recipe.Key];

            materials[i].Add(endMat);

            i++;
        }

        return materials;
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

[System.Serializable]
public class IngredientRadiatingMaterialDict : SerializableDictionaryBase<EItemType, Material> { }

public enum EItemState
{
    Normal,
    Freezed,
    Burned,

    Count,
}