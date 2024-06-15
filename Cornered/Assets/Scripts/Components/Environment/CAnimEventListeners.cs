/// <summary>
/// Filename: CAnimEventListeners.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAnimEventListeners : MonoBehaviour
{
    public void OnStepHappened()
    {
        EventManager.Raise(new StepHappenedEvent() { animEventCatcherTransform = transform });
    }
}
