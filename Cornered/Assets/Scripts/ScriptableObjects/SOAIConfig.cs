/// <summary>
/// Filename: SOAIConfig.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AI Config")]
public class SOAIConfig : ScriptableObject
{
    [SerializeField] private float m_HideWhenLifeLessThanPercentage;
    [SerializeField] private float m_AttackWhenLifeMoreThanPercentage;
    [SerializeField] private float m_PreservedDistanceBetweenPlayerAndMe;
    [SerializeField] private float m_NavmeshSamplePositionDistance;
    [SerializeField] private float m_RayTraverseStepSizeToDiscoverHidingPlace;

    public float hideWhenLifeLessThanPercentage => m_HideWhenLifeLessThanPercentage;

    public float preservedDistanceBetweenPlayerAndMe => m_PreservedDistanceBetweenPlayerAndMe;

    public float attackWhenLifeMoreThanPercentage => m_AttackWhenLifeMoreThanPercentage;

    public float navmeshSamplePositionDistance => m_NavmeshSamplePositionDistance;

    public float rayTraverseStepSizeToDiscoverHidingPlace => m_RayTraverseStepSizeToDiscoverHidingPlace;
}
