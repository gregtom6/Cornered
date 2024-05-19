using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CIngredient : MonoBehaviour, IPickable
{
    [SerializeField] private List<Collider> m_Colliders = new();

    //todo: ezeket ososztalyba

    public void Drop()
    {
        m_Colliders.ForEach(x => x.enabled = true);
    }

    public bool IsPicked()
    {
        return m_Colliders.TrueForAll(x => !x.enabled);
    }

    public void Pickup()
    {
        m_Colliders.ForEach(x => x.enabled = false);
    }


    void Start()
    {
        m_Colliders.ForEach(x => x.enabled = true);
    }
}
