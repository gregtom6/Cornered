/// <summary>
/// Filename: CPlayerAnimator.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerAnimator : CCharacterAnimator
{
    [SerializeField] private CInteractableDetector m_InteractableDetector;

    protected override bool IsDetectingInteractable()
    {
        return m_InteractableDetector.isValidHit;
    }
}
