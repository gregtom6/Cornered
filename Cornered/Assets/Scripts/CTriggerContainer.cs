using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTriggerContainer : MonoBehaviour
{
    public Action<Transform> objectEntered;

    public bool isInside => m_IsInside;

    private bool m_IsInside = false;

    private void OnTriggerEnter(Collider other)
    {
        m_IsInside = true;

        objectEntered?.Invoke(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        m_IsInside = false;
    }
}
