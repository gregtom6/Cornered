using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CPlayerController : CCharacterController
{
    [SerializeField] private GameInput m_GameInput;
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private Transform headParent;
    [SerializeField] private Transform bodyParent;
    [SerializeField] private Transform armParent;
    [SerializeField] private Transform legParent;

    private Vector3 m_Movement;
    private Vector2 m_Rot;

    private void OnPointerPosition(Vector2 obj)
    {
        m_Rot.x += obj.x * AllConfig.Instance.CharacterConfig.headRotSpeed;
        m_Rot.y += -obj.y * AllConfig.Instance.CharacterConfig.headRotSpeed;

        m_Rot.y = m_Rot.y > AllConfig.Instance.CharacterConfig.headMaxRotX ? AllConfig.Instance.CharacterConfig.headMaxRotX : m_Rot.y;
        m_Rot.y = m_Rot.y < AllConfig.Instance.CharacterConfig.headMinRotX ? AllConfig.Instance.CharacterConfig.headMinRotX : m_Rot.y;
    }

    private void OnLeftRightMovement(float obj)
    {
        m_Movement.x = obj * (AllConfig.Instance.CharacterConfig.runSpeed / 100f) * GetAdditionalMultiplier();

        movementState = EMovementState.Strafing;
    }

    private void OnForwardBackwardMovement(float obj)
    {
        m_Movement.z = obj * (AllConfig.Instance.CharacterConfig.runSpeed / 100f) * GetAdditionalMultiplier();

        movementState = EMovementState.Walking;
    }

    private float GetAdditionalMultiplier()
    {
        CurrentInventory currentInventory = InventoryManager.instance.GetCopyOfCurrentInventory(ECharacterType.Player);

        return currentInventory.additional.item == EItemType.FastBoots ? AllConfig.Instance.CharacterConfig.fastBootsSpeedMultiplier : 1f;
    }
    private void OnEnable()
    {
        if (m_GameInput != null)
        {
            m_GameInput.ForwardBackward += OnForwardBackwardMovement;
            m_GameInput.LeftRight += OnLeftRightMovement;
            m_GameInput.PointerDelta += OnPointerPosition;
        }
    }

    private void OnDisable()
    {
        if (m_GameInput != null)
        {
            m_GameInput.ForwardBackward -= OnForwardBackwardMovement;
            m_GameInput.LeftRight -= OnLeftRightMovement;
            m_GameInput.PointerDelta -= OnPointerPosition;
        }
    }

    private void Update()
    {
        ManageLooking();
    }

    private void FixedUpdate()
    {
        ActualPositionChange();
    }

    private void LateUpdate()
    {
        if (movementState != EMovementState.Standing && Mathf.Approximately(m_Movement.x, 0f) && Mathf.Approximately(m_Movement.z, 0f))
        {
            movementState = EMovementState.Standing;
        }
    }

    private void ActualPositionChange()
    {
        if (headParent != null && rigidbody != null)
        {
            Vector3 direction = headParent.TransformDirection(m_Movement);
            direction.y = 0f;
            Vector3 newPosition = rigidbody.position + direction * Time.deltaTime * 100f;
            rigidbody.MovePosition(newPosition);
        }
    }

    private void ManageLooking()
    {
        if (headParent != null && legParent != null && armParent != null && bodyParent != null)
        {
            headParent.localRotation = Quaternion.Euler(m_Rot.y, m_Rot.x, 0.0f);
            legParent.localRotation = Quaternion.Euler(0f, m_Rot.x, 0f);
            armParent.localRotation = Quaternion.Euler(0f, m_Rot.x, 0f);
            bodyParent.localRotation = Quaternion.Euler(0f, m_Rot.x, 0f);
        }
    }
}