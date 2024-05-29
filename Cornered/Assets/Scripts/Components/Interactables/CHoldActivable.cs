/// <summary>
/// Filename: CHoldActivable.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CHoldActivable : MonoBehaviour
{
    public abstract void HoldProcessStarted();

    public abstract void HoldProcessEnded();
}
