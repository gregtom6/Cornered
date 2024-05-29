/// <summary>
/// Filename: CCharacterController.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CCharacterController : MonoBehaviour
{
    protected EMovementState _movementState = EMovementState.Standing;

    protected EMovementState movementState
    {
        get
        {
            return _movementState;
        }
        set
        {
            if (_movementState == value)
                return;

            _movementState = value;
        }
    }

    public EMovementState characterMovementState => movementState;
}
