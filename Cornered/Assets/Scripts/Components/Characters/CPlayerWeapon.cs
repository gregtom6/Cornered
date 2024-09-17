/// <summary>
/// Filename: CPlayerWeapon.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class CPlayerWeapon : CWeapon
{
    [SerializeField] private GameInput m_GameInput;

    protected override EItemType GetEquippedWeapon()
    {
        CurrentInventory currentInventory = InventoryManager.instance.GetCopyOfCurrentInventory(ECharacterType.Player);

        return currentInventory.weapon.item;
    }

    private void OnEnable()
    {
        if (m_GameInput != null)
        {
            m_GameInput.LeftPointerDown += OnLeftPointerDown;
        }
    }

    private void OnDisable()
    {
        if (m_GameInput != null)
        {
            m_GameInput.LeftPointerDown -= OnLeftPointerDown;
        }
    }

    private void OnLeftPointerDown(Vector2 obj)
    {
        if (m_IsShootDisabled)
        {
            return;
        }

        ShootWithEquippedWeapon();
    }

    private void Update()
    {
        if (!IsThereEquippedWeapon() || m_IsReadyToShoot)
        {
            return;
        }

        SetReadyToShootAfterCooldownHappened(GetEquippedWeapon());
    }
}
