/// <summary>
/// Filename: CEquippedWeapon.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEquippedWeapon : MonoBehaviour
{
    [SerializeField] private Transform m_MuzzleTransform;

    public Transform muzzleTransform => m_MuzzleTransform;
}
