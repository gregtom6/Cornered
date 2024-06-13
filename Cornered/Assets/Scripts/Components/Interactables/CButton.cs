/// <summary>
/// Filename: CButton.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CAudioPlayer))]
public class CButton : MonoBehaviour, IInteractable
{
    public Action pressHappened;

    protected CAudioPlayer m_AudioPlayer;

    public virtual void Interact()
    {
        m_AudioPlayer.Play();
        pressHappened?.Invoke();
    }

    protected virtual void Start()
    {
        m_AudioPlayer = GetComponent<CAudioPlayer>();
    }
}
