using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CShotVisualRepresenter : ProjectileVisualizer
{
    [SerializeField] private Light m_Light;

    private new void Start()
    {
        m_Light.enabled = false;
    }
}