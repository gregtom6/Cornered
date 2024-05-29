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
    [SerializeField] private GameObject m_RecipeShowOperatorPrefab;
    [SerializeField] private RecipeDict m_RecipeDict;
    [SerializeField] private ProductPrefabDict m_ProductPrefabDict;
    [SerializeField] private IngredientRadiatingMaterialDict m_IngredientRadiatingMaterialDict;
    [SerializeField] private EffectRadiatingMaterialDict m_EffectRadiatingMaterialDict;

    public Material plusSignMaterial => m_RecipeShowPlusMaterial;
    public Material equalSignMaterial => m_RecipeShowEqualMaterial;

    public GameObject recipeShowElementPrefab => m_RecipeShowElementPrefab;

    public GameObject recipeShowOperatorPrefab => m_RecipeShowOperatorPrefab;

    public GameObject GetResultItem(IReadOnlyList<ItemDatas> itemTypes)
    {
        KeyValuePair<EItemType, ItemTypeDetails> item = m_RecipeDict.Where(x => AreListsEqual(itemTypes, x.Value.items)).FirstOrDefault();
        EItemType resultItemType = item.Key;
        if (m_ProductPrefabDict.ContainsKey(resultItemType))
        {
            return m_ProductPrefabDict[resultItemType];
        }

        return null;
    }

    public IReadOnlyList<IReadOnlyList<Material>> GetRadiatingMaterialsOfAllRecipes(out IReadOnlyList<IReadOnlyList<Material>> effectMaterials)
    {
        List<List<Material>> materials = new();
        List<List<Material>> itemStateMaterials = new();

        int i = 0;

        foreach (KeyValuePair<EItemType, ItemTypeDetails> recipe in m_RecipeDict)
        {
            if (!AllConfig.Instance.ProgressConfig.IsAbilityAlreadyUnlocked(recipe.Value.necessaryAbilityToUse))
            {
                continue;
            }

            materials.Add(new());
            itemStateMaterials.Add(new());

            foreach (ItemDatas itemTypes in recipe.Value.items)
            {
                Material mat = m_IngredientRadiatingMaterialDict[itemTypes.item];

                materials[i].Add(mat);

                Material stateMat = m_EffectRadiatingMaterialDict[itemTypes.state];

                itemStateMaterials[i].Add(stateMat);
            }

            Material endMat = m_IngredientRadiatingMaterialDict[recipe.Key];

            materials[i].Add(endMat);

            Material stateEndMat = m_EffectRadiatingMaterialDict[EItemState.Normal];

            itemStateMaterials[i].Add(stateEndMat);

            i++;
        }

        effectMaterials = itemStateMaterials;
        return materials;
    }

    //TODO: placing this into a utils class?

    public static bool AreListsEqual(IReadOnlyList<ItemDatas> list1, IReadOnlyList<ItemDatas> list2)
    {
        if (list1 == null || list2 == null)
            return list1 == list2;

        if (list1.Count != list2.Count)
            return false;

        var dict1 = list1.GroupBy(item => item).ToDictionary(g => g.Key, g => g.Count());
        var dict2 = list2.GroupBy(item => item).ToDictionary(g => g.Key, g => g.Count());

        return dict1.Count == dict2.Count && dict1.All(kv => dict2.TryGetValue(kv.Key, out int count) && count == kv.Value);
    }
}

[Serializable]
public class ItemTypeDetails
{
    public EAbility necessaryAbilityToUse;
    public List<ItemDatas> items = new();
}

[Serializable]
public class ItemDatas
{
    public EItemType item;
    public EItemState state;

    public ItemDatas(EItemType itemType, EItemState itemState)
    {
        item = itemType;
        state = itemState;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        ItemDatas other = (ItemDatas)obj;
        return item == other.item && state == other.state;
    }

    public override int GetHashCode()
    {
        int hashItem = item.GetHashCode();
        int hashState = state.GetHashCode();
        return hashItem ^ hashState;
    }

    public virtual void Equip(CurrentInventory inventory) { }
}

[Serializable]
public class WeaponItemDatas: ItemDatas
{
    public WeaponItemDatas(EItemType itemType, EItemState itemState) : base(itemType, itemState) { }

    public override void Equip(CurrentInventory inventory)
    {
        inventory.weapon = this;
    }
}

[Serializable]
public class ShieldItemDatas : ItemDatas
{
    public ShieldItemDatas(EItemType itemType, EItemState itemState) : base(itemType, itemState) { }

    public override void Equip(CurrentInventory inventory)
    {
        inventory.shield = this;
    }
}

[Serializable]
public class AdditionalItemDatas : ItemDatas
{
    public AdditionalItemDatas(EItemType itemType, EItemState itemState) : base(itemType, itemState) { }

    public override void Equip(CurrentInventory inventory)
    {
        inventory.additional = this;
    }
}

[System.Serializable]
public class RecipeDict : SerializableDictionaryBase<EItemType, ItemTypeDetails> { }

[System.Serializable]
public class ProductPrefabDict : SerializableDictionaryBase<EItemType, GameObject> { }

[System.Serializable]
public class IngredientRadiatingMaterialDict : SerializableDictionaryBase<EItemType, Material> { }

[System.Serializable]
public class EffectRadiatingMaterialDict : SerializableDictionaryBase<EItemState, Material> { }

