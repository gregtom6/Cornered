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
    protected EMovementState m_MovementState = EMovementState.Standing;

    public EMovementState characterMovementState => m_MovementState;
}
