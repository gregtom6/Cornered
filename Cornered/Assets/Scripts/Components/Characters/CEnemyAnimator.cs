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
