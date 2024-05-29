/// <summary>
/// Filename: SOExitDoorConfig.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Exit Door Config")]
public class SOExitDoorConfig : ScriptableObject
{
    [SerializeField] private float m_ButtonHoldingOpenMultiplier;
    [SerializeField] private float m_MinPercentage;
    [SerializeField] private float m_MaxPercentage;

    public float buttonHoldingOpenMultiplier => m_ButtonHoldingOpenMultiplier;

    public float minPercentage => m_MinPercentage;
    public float maxPercentage => m_MaxPercentage;
}
