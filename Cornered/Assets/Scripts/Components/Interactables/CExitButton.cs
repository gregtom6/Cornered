/// <summary>
/// Filename: CExitButton.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CExitButton : MonoBehaviour, IHoldable
{
    [SerializeField] private CHoldActivable m_HoldActivable;

    public void HoldingFinished()
    {
        m_HoldActivable.HoldProcessEnded();
    }

    public void HoldingStarted()
    {
        m_HoldActivable.HoldProcessStarted();
    }
}
