using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HoldActivable: MonoBehaviour
{
    public abstract void HoldProcessStarted();

    public abstract void HoldProcessEnded();
}
