using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CWeapon : MonoBehaviour
{
    [SerializeField] protected CShotDetector m_ShotDetector;
    [SerializeField] protected List<ProjectileVisualizer> m_ProjectileVisualizers = new();
    public bool isReadyToShoot => m_IsReadyToShoot;

    protected float m_CooldownStartTime;

    protected bool m_IsReadyToShoot;

    public float GetCooldownTimeLeftPercentageBetween01()
    {
        float currentTime = Time.time - m_CooldownStartTime;

        EItemType usedWeapon = GetEquippedWeapon();

        WeaponSettings weaponSettings = AllConfig.Instance.WeaponConfig.GetWeaponSettings(usedWeapon);

        return Mathf.Clamp01(currentTime / weaponSettings.cooldownTimeInSec);
    }

    public abstract bool IsThereEquippedWeapon();

    protected void SetReadyToShootAfterCooldownHappened(EItemType usedWeapon)
    {
        float currentTime = Time.time - m_CooldownStartTime;

        WeaponSettings weaponSettings = AllConfig.Instance.WeaponConfig.GetWeaponSettings(usedWeapon);

        if (currentTime >= weaponSettings.cooldownTimeInSec)
        {
            m_IsReadyToShoot = true;
        }
    }

    protected void ShootWithEquippedWeapon()
    {
        if (!IsThereEquippedWeapon() || !m_IsReadyToShoot)
        {
            return;
        }

        m_ProjectileVisualizers.ForEach(x => x.Show());

        m_IsReadyToShoot = false;

        m_CooldownStartTime = Time.time;

        if (m_ShotDetector.isValidHit)
        {
            if (IsDetectingHealthComponentDirectly(out CHealth health))
            {
                EItemType playerWeapon = GetEquippedWeapon();

                WeaponSettings weaponSettings = AllConfig.Instance.WeaponConfig.GetWeaponSettings(playerWeapon);

                health.DamageHealth(weaponSettings.damage);
            }
        }
    }

    protected bool IsDetectingHealthComponentDirectly(out CHealth health)
    {
        RaycastHit raycastHit = m_ShotDetector.raycastHit;
        health = raycastHit.collider.GetComponentInParent<CHealth>();

        if (health == null)
        {
            health = raycastHit.collider.GetComponentInChildren<CHealth>();
        }
        
        return health != null;
    }

    protected abstract EItemType GetEquippedWeapon();
}
