/// <summary>
/// Filename: CEquippedWeapon.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections.Generic;
using UnityEngine;

public class CEquippedWeapon : MonoBehaviour
{
    [SerializeField] private Transform m_MuzzleTransform;
    [SerializeField] private List<CAudioPlayer> m_AudioPlayers = new();

    public Transform muzzleTransform => m_MuzzleTransform;

    public IReadOnlyList<CAudioPlayer> audioPlayers => m_AudioPlayers;
}
