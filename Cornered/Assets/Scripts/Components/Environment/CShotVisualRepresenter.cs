/// <summary>
/// Filename: CShotVisualRepresenter.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CShotVisualRepresenter : CProjectileVisualizer
{
    [SerializeField] private Light m_Light;

    private void Start()
    {
        m_Light.enabled = false;
    }
}
