/// <summary>
/// Filename: CEnemyWeapon.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyWeapon : CWeapon
{
    protected override EItemType GetEquippedWeapon()
    {
        CurrentInventory currentInventory = InventoryManager.instance.GetCopyOfCurrentInventory(ECharacterType.Enemy);

        return currentInventory.weapon.item;
    }

    private void Update()
    {
        if (m_IsShootDisabled)
        {
            return;
        }

        if (m_IsReadyToShoot)
        {
            if (m_ShotDetector.isValidHit && IsDetectingHealthComponentDirectly(out CHealth health))
            {
                ShootWithEquippedWeapon();
            }
        }
        else
        {
            SetReadyToShootAfterCooldownHappened(GetEquippedWeapon());
        }
    }
}
