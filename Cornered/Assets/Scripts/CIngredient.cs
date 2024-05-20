using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CIngredient : MonoBehaviour, IPickable
{
    [SerializeField] protected EItemType m_ItemType = EItemType.Count;
    [SerializeField] protected EItemState m_ItemState = EItemState.Count;
    [SerializeField] protected List<Collider> m_Colliders = new();
    [SerializeField] protected List<Rigidbody> m_Rigidbodies= new();

    //todo: ezeket ososztalyba

    private bool m_WasPickedAnyTime;

    public void Drop()
    {
        m_Colliders.ForEach(x => x.enabled = true);
        m_Rigidbodies.ForEach(x =>
        {
            x.useGravity = true;
            x.isKinematic = false;
        });

        transform.SetParent(null);
    }

    public bool IsPicked()
    {
        return m_Colliders.TrueForAll(x => !x.enabled);
    }

    public bool WasPickedAnytime()
    {
        return m_WasPickedAnyTime;
    }

    public void Pickup(Transform pickerTransform)
    {
        m_Colliders.ForEach(x => x.enabled = false);
        m_Rigidbodies.ForEach(x =>
        {
            x.useGravity = false;
            x.isKinematic = true;
        });

        transform.SetParent(pickerTransform);

        m_WasPickedAnyTime = true;
    }

    void Start()
    {
        m_Colliders.ForEach(x => x.enabled = true);
        m_Rigidbodies.ForEach(x =>
        {
            x.useGravity = true;
            x.isKinematic = false;
        });
    }

    public virtual IEquippable GetEquippable()
    {
        return null;
    }
}
