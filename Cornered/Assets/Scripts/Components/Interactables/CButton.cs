/// <summary>
/// Filename: CButton.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CButton : MonoBehaviour, IInteractable
{
    public Action pressHappened;

    public virtual void Interact()
    {
        pressHappened?.Invoke();
    }
}
