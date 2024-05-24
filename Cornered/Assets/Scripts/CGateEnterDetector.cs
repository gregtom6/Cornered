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
