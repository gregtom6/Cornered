using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class CPlayerWeapon : CWeapon
{
    [SerializeField] private GameInput m_GameInput;

    public override bool IsThereEquippedWeapon()
    {
        return GetEquippedWeapon() != EItemType.Count;
    }

    protected override EItemType GetEquippedWeapon()
    {
        return InventoryManager.instance.currentPlayerWeapon.item;
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
