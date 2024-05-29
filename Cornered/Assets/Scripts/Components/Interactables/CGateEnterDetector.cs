/// <summary>
/// Filename: CGateEnterDetector.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGateEnterDetector : MonoBehaviour
{
    public Action enterHappened;

    private void OnTriggerEnter(Collider other)
    {
        enterHappened?.Invoke();
    }
}
