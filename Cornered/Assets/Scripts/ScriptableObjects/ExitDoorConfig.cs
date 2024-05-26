using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Exit Door Config")]
public class ExitDoorConfig : ScriptableObject
{
    [SerializeField] private float m_ButtonHoldingOpenMultiplier;
    [SerializeField] private float m_MinPercentage;
    [SerializeField] private float m_MaxPercentage;

    public float buttonHoldingOpenMultiplier => m_ButtonHoldingOpenMultiplier;

    public float minPercentage => m_MinPercentage;
    public float maxPercentage => m_MaxPercentage;
}
