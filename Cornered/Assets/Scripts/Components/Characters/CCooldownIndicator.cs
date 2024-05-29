/// <summary>
/// Filename: CCooldownIndicator.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CWeapon))]
public class CCooldownIndicator : MonoBehaviour
{
    [SerializeField] private Transform m_IndicatorTransform;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;
    private CWeapon m_Weapon;
    private Vector3 m_TemporaryScaleVector;

    private void Start()
    {
        m_Weapon = GetComponent<CWeapon>();
    }

    private void Update()
    {
        if (!m_Weapon.IsThereEquippedWeapon())
        {
            return;
        }

        float percentage = m_Weapon.GetCooldownTimeLeftPercentageBetween01();

        m_IndicatorTransform.localScale = new Vector3(m_IndicatorTransform.localScale.x, percentage, m_IndicatorTransform.localScale.z);

        m_SpriteRenderer.color = percentage >= 1f ? Color.green : Color.red;
    }
}
