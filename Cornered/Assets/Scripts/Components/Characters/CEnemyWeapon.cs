using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyWeapon : CWeapon
{
    public override bool IsThereEquippedWeapon()
    {
        return GetEquippedWeapon() != EItemType.Count;
    }

    protected override EItemType GetEquippedWeapon()
    {
        CurrentInventory currentInventory = InventoryManager.instance.GetCopyOfCurrentInventory(ECharacterType.Enemy);

        return currentInventory.weapon.item;
    }

    private void Update()
    {
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
