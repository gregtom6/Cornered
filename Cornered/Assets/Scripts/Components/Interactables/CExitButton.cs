/// <summary>
/// Filename: CExitButton.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CAudioPlayer))]
public class CExitButton : MonoBehaviour, IHoldable
{
    [SerializeField] private CHoldActivable m_HoldActivable;

    private CAudioPlayer m_AudioPlayer;

    public void HoldingFinished()
    {
        m_HoldActivable.HoldProcessEnded();
    }

    public void HoldingStarted()
    {
        m_AudioPlayer.Play();

        m_HoldActivable.HoldProcessStarted();
    }

    private void Start()
    {
        m_AudioPlayer = GetComponent<CAudioPlayer>();
    }
}
