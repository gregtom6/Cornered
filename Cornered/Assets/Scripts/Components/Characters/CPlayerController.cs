﻿/// <summary>
/// Filename: CPlayerController.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

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
    private bool m_ShotReceived;
    private float m_TimeWhenShotReceived;
    private float m_SelectedZRotation;

    private void OnPointerPosition(Vector2 obj)
    {
        m_Rot.x += obj.x * AllConfig.Instance.CharacterConfig.headRotSpeed;
        m_Rot.y += -obj.y * AllConfig.Instance.CharacterConfig.headRotSpeed;

        m_Rot.y = m_Rot.y > AllConfig.Instance.CharacterConfig.headMaxRotX ? AllConfig.Instance.CharacterConfig.headMaxRotX : m_Rot.y;
        m_Rot.y = m_Rot.y < AllConfig.Instance.CharacterConfig.headMinRotX ? AllConfig.Instance.CharacterConfig.headMinRotX : m_Rot.y;
    }

    private void OnLeftRight(float obj)
    {
        m_Movement.x = obj * (AllConfig.Instance.CharacterConfig.runSpeed / 100f) * GetAdditionalMultiplier();

        m_MovementState = EMovementState.Strafing;
    }

    private void OnForwardBackward(float obj)
    {
        m_Movement.z = obj * (AllConfig.Instance.CharacterConfig.runSpeed / 100f) * GetAdditionalMultiplier();

        m_MovementState = EMovementState.Walking;
    }

    private void OnCameraUpDown(float obj)
    {
        m_Rot.y += -obj * AllConfig.Instance.CharacterConfig.headRotSpeed;

        m_Rot.y = m_Rot.y > AllConfig.Instance.CharacterConfig.headMaxRotX ? AllConfig.Instance.CharacterConfig.headMaxRotX : m_Rot.y;
        m_Rot.y = m_Rot.y < AllConfig.Instance.CharacterConfig.headMinRotX ? AllConfig.Instance.CharacterConfig.headMinRotX : m_Rot.y;
    }

    private void OnCameraLeftRight(float obj)
    {
        Debug.Log("");
        m_Rot.x += obj * AllConfig.Instance.CharacterConfig.headRotSpeed;
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
            m_GameInput.ForwardBackward += OnForwardBackward;
            m_GameInput.LeftRight += OnLeftRight;
            m_GameInput.CameraUpDown += OnCameraUpDown;
            m_GameInput.CameraLeftRight += OnCameraLeftRight;
            m_GameInput.PointerDelta += OnPointerPosition;
        }

        EventManager.AddListener<CharacterReceivedShotEvent>(OnCharacterReceivedShotEvent);
    }

    private void OnDisable()
    {
        if (m_GameInput != null)
        {
            m_GameInput.ForwardBackward -= OnForwardBackward;
            m_GameInput.LeftRight -= OnLeftRight;
            m_GameInput.CameraUpDown -= OnCameraUpDown;
            m_GameInput.CameraLeftRight -= OnCameraLeftRight;
            m_GameInput.PointerDelta -= OnPointerPosition;
        }

        EventManager.RemoveListener<CharacterReceivedShotEvent>(OnCharacterReceivedShotEvent);
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
        if (m_MovementState != EMovementState.Standing && Mathf.Approximately(m_Movement.x, 0f) && Mathf.Approximately(m_Movement.z, 0f))
        {
            m_MovementState = EMovementState.Standing;
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
        if (m_GameInput.upDownPressed)
        {
            OnCameraUpDown(m_GameInput.upDownFloat);
        }

        if (m_GameInput.leftRightPressed)
        {
            OnCameraLeftRight(m_GameInput.leftRightFloat);
        }


        if (headParent != null && legParent != null && armParent != null && bodyParent != null)
        {
            headParent.localRotation = Quaternion.Euler(m_Rot.y, m_Rot.x, GetZRotationValue());
            legParent.localRotation = Quaternion.Euler(0f, m_Rot.x, 0f);
            armParent.localRotation = Quaternion.Euler(0f, m_Rot.x, 0f);
            bodyParent.localRotation = Quaternion.Euler(0f, m_Rot.x, 0f);
        }
    }

    private float GetZRotationValue()
    {
        if (!m_ShotReceived)
        {
            return 0f;
        }

        float currentTime = Time.time - m_TimeWhenShotReceived;
        float percent = currentTime / AllConfig.Instance.TimeConfig.receivingHitPostProcessTime;
        percent = Mathf.Clamp01(percent);

        if (percent >= 1f)
        {
            m_ShotReceived = false;
        }

        return Mathf.Lerp(m_SelectedZRotation, 0f, percent);
    }

    private void OnCharacterReceivedShotEvent(CharacterReceivedShotEvent ev)
    {
        if (ev.charType == ECharacterType.Enemy)
        {
            return;
        }

        m_ShotReceived = true;
        m_TimeWhenShotReceived = Time.time;

        bool randomBool = Mathf.Round(UnityEngine.Random.value) == 1;
        m_SelectedZRotation = randomBool ? AllConfig.Instance.CharacterConfig.whenReceivingShotHeadXRotationMin : AllConfig.Instance.CharacterConfig.whenReceivingShotHeadXRotationMax;
    }
}