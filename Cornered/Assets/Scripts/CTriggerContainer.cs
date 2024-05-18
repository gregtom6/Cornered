using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTriggerContainer : MonoBehaviour
{
    private HashSet<Collider> m_Colliders = new();

    public Action<Transform> objectEntered;

    public Action allObjectLeft;

    private void OnTriggerEnter(Collider other)
    {
        m_Colliders.Add(other);

        objectEntered?.Invoke(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        m_Colliders.Remove(other);

        if (m_Colliders.Count == 0)
        {
            allObjectLeft?.Invoke();
        }
    }
}
