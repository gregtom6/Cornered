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
