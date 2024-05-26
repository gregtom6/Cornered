using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CExitButton : MonoBehaviour, IHoldable
{
    [SerializeField] private HoldActivable m_HoldActivable;

    public void HoldingFinished()
    {
        m_HoldActivable.HoldProcessEnded();
    }

    public void HoldingStarted()
    {
        m_HoldActivable.HoldProcessStarted();
    }
}
