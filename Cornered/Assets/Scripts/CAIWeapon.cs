using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAIWeapon : MonoBehaviour
{
    [SerializeField] private CShotDetector m_ShotDetector;
    [SerializeField] private List<ProjectileVisualizer> m_ProjectileVisualizers = new();

    private float m_CooldownStartTime;

    private bool m_IsReadyToShoot;

    public bool isReadyToShoot => m_IsReadyToShoot;

    private void Update()
    {
        if (m_IsReadyToShoot)
        {
            if (m_ShotDetector.isValidHit)
            {
                RaycastHit raycastHit = m_ShotDetector.raycastHit;

                CHealth health = raycastHit.collider.GetComponentInParent<CHealth>();

                if (health != null)
                {
                    ShootWithEquippedWeapon(health);
                }
            }
        }
        else
        {
            float currentTime = Time.time - m_CooldownStartTime;

            EItemType equippedWeapon = InventoryManager.instance.currentEnemyWeapon.item;

            WeaponSettings weaponSettings = AllConfig.Instance.WeaponConfig.GetWeaponSettings(equippedWeapon);

            if (currentTime >= weaponSettings.cooldownTimeInSec)
            {
                m_IsReadyToShoot = true;
            }
        }
    }

    private void ShootWithEquippedWeapon(CHealth health)
    {
        m_ProjectileVisualizers.ForEach(x => x.Show());

        m_IsReadyToShoot = false;

        m_CooldownStartTime = Time.time;

        EItemType equippedWeapon = InventoryManager.instance.currentEnemyWeapon.item;

        WeaponSettings weaponSettings = AllConfig.Instance.WeaponConfig.GetWeaponSettings(equippedWeapon);

        health.DamageHealth(weaponSettings.damage);
    }
}
