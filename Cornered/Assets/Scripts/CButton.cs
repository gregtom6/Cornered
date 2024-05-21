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
