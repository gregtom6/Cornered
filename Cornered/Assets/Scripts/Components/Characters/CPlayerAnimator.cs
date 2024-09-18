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
