using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private GameInput m_GameInput;
    [SerializeField] new Rigidbody rigidbody;
    [SerializeField] Transform headParent;
    [SerializeField] Transform bodyParent;
    [SerializeField] Transform armParent;
    [SerializeField] Transform legParent;
    [SerializeField] Animator bodyAnimator;
    [SerializeField] Animator armAnimator;
    [SerializeField] Animator legAnimator;
    [SerializeField] string isMovingAnimKey;
    [SerializeField] string isStrafingAnimKey;
    [SerializeField] string standingAnimKey;
    [SerializeField] string rotateAnimKey;
    [SerializeField] float runSpeed;
    [SerializeField] float headMaxRotX;
    [SerializeField] float headMinRotX;

    private Vector3 m_Movement;
    private Vector2 m_Rot;

    protected MovementState _movementState = MovementState.Standing;

    protected MovementState movementState
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

    private void OnEnable()
    {
        if (m_GameInput != null)
        {
            m_GameInput.ForwardBackwardMovement += OnForwardBackwardMovement;
            m_GameInput.LeftRightMovement += OnLeftRightMovement;
            m_GameInput.PointerPosition += OnPointerPosition;
        }
    }

    private void OnPointerPosition(Vector2 obj)
    {
        m_Rot.x += obj.x;
        m_Rot.y += -obj.y;

        if (m_Rot.y > headMaxRotX)
            m_Rot.y = headMaxRotX;
        if (m_Rot.y < headMinRotX)
            m_Rot.y = headMinRotX;
    }

    private void OnLeftRightMovement(float obj)
    {
        m_Movement.x = obj * (runSpeed / 100f);

        movementState = MovementState.Strafing;
    }

    private void OnForwardBackwardMovement(float obj)
    {
        m_Movement.z = obj * (runSpeed / 100f);

        movementState = MovementState.Moving;
    }

    private void OnDisable()
    {
        if (m_GameInput != null)
        {
            m_GameInput.ForwardBackwardMovement -= OnForwardBackwardMovement;
            m_GameInput.LeftRightMovement -= OnLeftRightMovement;
        }
    }

    protected virtual void Update()
    {
        ManageLooking();
    }

    protected virtual void FixedUpdate()
    {
        ActualPositionChange();
    }

    protected void ActualPositionChange()
    {
        m_Movement.y = 0;

        if (headParent != null && rigidbody != null)
        {
            Vector3 direction = headParent.TransformDirection(m_Movement);
            Vector3 newPosition = rigidbody.position + direction * Time.deltaTime * 100;
            rigidbody.MovePosition(newPosition);

        }
    }

    protected void ManageLooking()
    {
        if (headParent != null && legParent != null && armParent != null && bodyParent != null)
        {
            headParent.localRotation = Quaternion.Euler(m_Rot.y, m_Rot.x, 0.0f);
            legParent.localRotation = Quaternion.Euler(0f, m_Rot.x, 0f);
            armParent.localRotation = Quaternion.Euler(0f, m_Rot.x, 0f);
            bodyParent.localRotation = Quaternion.Euler(0f, m_Rot.x, 0f);
        }
    }

    public enum MovementState
    {
        Standing,
        Moving,
        Strafing,
    }
}