/// <summary>
/// Filename: CWeapon.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CWeapon : MonoBehaviour
{
    [SerializeField] protected CShotDetector m_ShotDetector;
    [SerializeField] protected List<CProjectileVisualizer> m_ProjectileVisualizers = new();
    public bool isReadyToShoot => m_IsReadyToShoot;

    protected float m_CooldownStartTime;
    protected bool m_IsReadyToShoot;
    protected IReadOnlyList<CAudioPlayer> m_EquippedAudioPlayers;

    public float GetCooldownTimeLeftPercentageBetween01()
    {
        float currentTime = Time.time - m_CooldownStartTime;

        EItemType usedWeapon = GetEquippedWeapon();

        WeaponSettings weaponSettings = AllConfig.Instance.EquipmentConfig.GetWeaponSettings(usedWeapon);

        return Mathf.Clamp01(currentTime / weaponSettings.cooldownTimeInSec);
    }

    public bool IsThereEquippedWeapon()
    {
        return GetEquippedWeapon() != EItemType.Count;
    }

    public void SetAudioPlayers(IReadOnlyList<CAudioPlayer> audioPlayers)
    {
        m_EquippedAudioPlayers = audioPlayers;
    }

    protected void SetReadyToShootAfterCooldownHappened(EItemType usedWeapon)
    {
        float currentTime = Time.time - m_CooldownStartTime;

        WeaponSettings weaponSettings = AllConfig.Instance.EquipmentConfig.GetWeaponSettings(usedWeapon);

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

        ManageAudioVisual();

        m_IsReadyToShoot = false;

        m_CooldownStartTime = Time.time;

        if (m_ShotDetector.isValidHit)
        {
            if (IsDetectingHealthComponentDirectly(out CHealth health))
            {
                EItemType playerWeapon = GetEquippedWeapon();

                WeaponSettings weaponSettings = AllConfig.Instance.EquipmentConfig.GetWeaponSettings(playerWeapon);

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

    private void ManageAudioVisual()
    {
        m_ProjectileVisualizers.ForEach(x => x.Show());

        for (int i = 0; i < m_EquippedAudioPlayers.Count; i++)
        {
            m_EquippedAudioPlayers[i].Play(m_EquippedAudioPlayers[i].transform);
        }
    }
}
