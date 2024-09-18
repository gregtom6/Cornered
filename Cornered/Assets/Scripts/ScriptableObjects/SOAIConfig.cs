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
    [SerializeField] private float m_AngleRotationChecksToDetectHidingSpot;
    [SerializeField] private float m_RayLengthToFindObstacle;
    [SerializeField] private float m_ObstacleFindingRayStartingStepCount;
    [SerializeField] private float m_ObstacleFindingRayMaxDistance;
    [SerializeField] private float m_ObstacleFindingRayDeltaStepSize;
    [SerializeField] private Vector3 m_HideSpotFinderOriginOffset;

    public float hideWhenLifeLessThanPercentage => m_HideWhenLifeLessThanPercentage;

    public float preservedDistanceBetweenPlayerAndMe => m_PreservedDistanceBetweenPlayerAndMe;

    public float attackWhenLifeMoreThanPercentage => m_AttackWhenLifeMoreThanPercentage;

    public float navmeshSamplePositionDistance => m_NavmeshSamplePositionDistance;

    public float rayTraverseStepSizeToDiscoverHidingPlace => m_RayTraverseStepSizeToDiscoverHidingPlace;

    public float angleRotationChecksToDetectHidingSpot => m_AngleRotationChecksToDetectHidingSpot;

    public float rayLengthToFindObstacle => m_RayLengthToFindObstacle;  

    public float obstacleFindingRayStartingStepCount => m_ObstacleFindingRayStartingStepCount;

    public float obstacleFindingRayMaxDistance => m_ObstacleFindingRayMaxDistance;

    public float obstacleFindingRayDeltaStepSize => m_ObstacleFindingRayDeltaStepSize;

    public Vector3 hideSpotFinderOriginOffset => m_HideSpotFinderOriginOffset;
}
