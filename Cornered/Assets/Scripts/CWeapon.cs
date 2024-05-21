using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

//TODO: osszevonni CAIWeaponnel

public class CWeapon : MonoBehaviour
{
    [SerializeField] private GameInput m_GameInput;
    [SerializeField] private CShotDetector m_ShotDetector;
    [SerializeField] private List<ProjectileVisualizer> m_ProjectileVisualizers = new();

    private float m_CooldownStartTime;

    private bool m_IsReadyToShoot;

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

    private bool IsThereEquippedWeapon()
    {
        return InventoryManager.instance.currentPlayerWeapon.item != EItemType.Count;
    }

    private void OnLeftPointerDown(Vector2 obj)
    {
        ShootWithEquippedWeapon();
    }

    private void ShootWithEquippedWeapon()
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
            RaycastHit raycastHit = m_ShotDetector.raycastHit;

            CHealth health = raycastHit.collider.GetComponentInParent<CHealth>();

            if (health != null)
            {
                EItemType playerWeapon = InventoryManager.instance.currentPlayerWeapon.item;

                WeaponSettings weaponSettings = AllConfig.Instance.WeaponConfig.GetWeaponSettings(playerWeapon);

                health.DamageHealth(weaponSettings.damage);
            }
        }
    }

    private void Update()
    {
        if (!IsThereEquippedWeapon() || m_IsReadyToShoot)
        {
            return;
        }

        float currentTime = Time.time - m_CooldownStartTime;

        EItemType playerWeapon = InventoryManager.instance.currentPlayerWeapon.item;

        WeaponSettings weaponSettings = AllConfig.Instance.WeaponConfig.GetWeaponSettings(playerWeapon);

        if (currentTime >= weaponSettings.cooldownTimeInSec)
        {
            m_IsReadyToShoot = true;
        }
    }
}
