using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMixingItemDetector : MonoBehaviour
{
    private HashSet<Collider> m_Colliders = new();

    public IReadOnlyList<ItemDatas> GetDetectedItems()
    {
        List<ItemDatas> itemTypes = new();

        foreach (Collider collider in m_Colliders)
        {
            CIngredient ingredient = collider.GetComponentInParent<CIngredient>();
            if (ingredient != null)
            {
                itemTypes.Add(new ItemDatas(ingredient.itemType, ingredient.itemState));
            }
        }

        return itemTypes;
    }

    public void DestroyAllItems()
    {
        foreach (Collider collider in m_Colliders)
        {
            Destroy(collider.gameObject);
        }

        m_Colliders.Clear();
    }

    public void BurnAllItems()
    {
        ChangeStateOfAllItems(EItemState.Burned);
    }

    public void FreezeAllItems()
    {
        ChangeStateOfAllItems(EItemState.Freezed);
    }

    private void ChangeStateOfAllItems(EItemState state)
    {
        foreach (Collider collider in m_Colliders)
        {
            CIngredient ingredient = collider.GetComponentInParent<CIngredient>();
            if (ingredient != null)
            {
                ingredient.SetState(state);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        m_Colliders.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        m_Colliders.Remove(other);
    }
}
