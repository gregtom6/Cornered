/// <summary>
/// Filename: CEnemyAnimator.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyAnimator : CCharacterAnimator
{
    protected override bool IsDetectingInteractable()
    {
        return false;
    }
}
