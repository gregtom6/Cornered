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

    private Vector3 m_Movement;
    private Vector2 m_Rot;
    private Vector2 m_PrevMousePos = Vector2.zero;

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
        Vector2 delta = m_PrevMousePos - obj;

        m_Rot.x += -delta.x * AllConfig.Instance.CharacterConfig.headRotSpeed;
        m_Rot.y += delta.y * AllConfig.Instance.CharacterConfig.headRotSpeed;

        m_PrevMousePos = obj;

        m_Rot.y = m_Rot.y > AllConfig.Instance.CharacterConfig.headMaxRotX ? AllConfig.Instance.CharacterConfig.headMaxRotX : m_Rot.y;
        m_Rot.y = m_Rot.y < AllConfig.Instance.CharacterConfig.headMinRotX ? AllConfig.Instance.CharacterConfig.headMinRotX : m_Rot.y;
    }

    private void OnLeftRightMovement(float obj)
    {
        m_Movement.x = obj * (AllConfig.Instance.CharacterConfig.runSpeed / 100f);

        movementState = MovementState.Strafing;
    }

    private void OnForwardBackwardMovement(float obj)
    {
        m_Movement.z = obj * (AllConfig.Instance.CharacterConfig.runSpeed / 100f);

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