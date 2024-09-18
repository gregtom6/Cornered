/// <summary>
/// Filename: CEnemyControllerHideSpotFinder.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public partial class CEnemyController : CCharacterController
{
    public class ObstacleHideSpots
    {
        public Transform obstacle;
        public List<float> angles = new();
        public List<Vector3> possibleHideSpots = new();
        public Vector3 highlightedHideSpot;
    }

    private class HideSpotFinder
    {
        private Transform m_MovementTargetPoint;
        private Transform m_PlayerTransform;
        private Transform m_EnemyTransform;
        private LayerMask m_PillarLayerMask;
        private LayerMask m_PlayerLayerMask;

        public HideSpotFinder(Transform movementTargetPoint, Transform playerTransform, Transform enemyTransform, LayerMask playerLayerMask, LayerMask pillarLayerMask)
        {
            m_MovementTargetPoint = movementTargetPoint;
            m_PlayerTransform = playerTransform;
            m_EnemyTransform = enemyTransform;
            m_PillarLayerMask = pillarLayerMask;
            m_PlayerLayerMask = playerLayerMask;
        }

        private bool IsThisPointOutsideColliders(Vector3 currentPoint)
        {
            return !HidingRoomElementsCollector.Instance.IsAnyColliderContainsPosition(currentPoint);
        }

        private bool IsThisPointNotVisibleByPlayer(Vector3 currentPoint)
        {
            Vector3 direction = CCharacterManager.instance.playerTransform.position - currentPoint;

            float distance = direction.magnitude;
            direction.Normalize();

#if UNITY_EDITOR
            DebugArrow.ForDebug(currentPoint, direction * distance, Color.yellow);
#endif

            if (Physics.Raycast(currentPoint, direction, out RaycastHit hit, distance, m_PlayerLayerMask))
            {
                return true;
            }

            return false;
        }

        private bool ThisRayIsNotHittingPlayer(RaycastHit raycastHits)
        {
            return raycastHits.transform != CCharacterManager.instance.playerTransform;
        }

        public Vector3? GetClosestHidingSpot()
        {
            float currentAngle = 0f;
            Vector3? selectedHideSpot = null;
            List<ObstacleHideSpots> possibleHideSpots = new();

            do
            {
                RaycastHit raycastHits = MakeRaycastInSelectedAngle(currentAngle, AllConfig.Instance.AIConfig.rayLengthToFindObstacle, out Ray rayToUse, out bool isHit);

                if (isHit && ThisRayIsNotHittingPlayer(raycastHits))
                {
                    ObstacleHideSpots loadedObstacleDetails = new ObstacleHideSpots();
                    int foundInIndex = -1;
                    if (IsObstacleAlreadyFound(raycastHits.transform, possibleHideSpots, out loadedObstacleDetails, out foundInIndex))
                    {
                        FindingPossiblePositionsAlongCurrentRay(raycastHits.point, rayToUse.direction, loadedObstacleDetails.possibleHideSpots);
                        loadedObstacleDetails.angles.Add(currentAngle);
                        possibleHideSpots[foundInIndex] = loadedObstacleDetails;
                    }
                    else
                    {
                        FindingPossiblePositionsAlongCurrentRay(raycastHits.point, rayToUse.direction, loadedObstacleDetails.possibleHideSpots);
                        loadedObstacleDetails.obstacle = raycastHits.transform;
                        loadedObstacleDetails.angles.Add(currentAngle);
                        possibleHideSpots.Add(loadedObstacleDetails);
                    }

                }

                currentAngle += AllConfig.Instance.AIConfig.angleRotationChecksToDetectHidingSpot;
            }
            while (currentAngle < 360f);

            if (possibleHideSpots.Count > 0)
            {
                FillHighlightedHideSpots(possibleHideSpots);

                List<Vector3> highlightedHideSpots = GetHighlightedHideSpots(possibleHideSpots);

                SortHitsBasedOnDistance(highlightedHideSpots);

                selectedHideSpot= highlightedHideSpots[0];
                m_MovementTargetPoint.position = highlightedHideSpots[0];
            }

            return selectedHideSpot;
        }

        List<Vector3> GetHighlightedHideSpots(List<ObstacleHideSpots> obstacles)
        {
            List<Vector3> highlightedHideSpots = new();

            for (int i = 0; i < obstacles.Count; i++)
            {
                highlightedHideSpots.Add(obstacles[i].highlightedHideSpot);
            }

            return highlightedHideSpots;
        }

        private void FillHighlightedHideSpots(List<ObstacleHideSpots> obstacles)
        {
            for (int i = 0; i < obstacles.Count; i++)
            {
                if (obstacles[i].possibleHideSpots.Count > 0)
                {
                    List<float> NormalizedAngles = new();

                    for (int j = 0; j < obstacles[i].angles.Count; j++)
                    {
                        NormalizedAngles.Add(NormalizeAngle(obstacles[i].angles[j]));
                    }

                    float MeanAngle = CalculateCircularMean(NormalizedAngles);

                    int closestAngleIndex = GetClosestIndex(NormalizedAngles, MeanAngle);

                    if (closestAngleIndex >= 0)
                    {
                        Vector3 middleElement = obstacles[i].possibleHideSpots[closestAngleIndex];
                        obstacles[i].highlightedHideSpot = middleElement;
                    }
                }
            }
        }

        private int GetClosestIndex(List<float> normalizedAngles, float targetValue)
        {
            if (normalizedAngles.Count == 0)
            {
                return -1;
            }

            int ClosestIndex = 0;
            float ClosestDistance = Mathf.Abs(normalizedAngles[0] - targetValue);

            for (int i = 1; i < normalizedAngles.Count; ++i)
            {
                float CurrentDistance = Mathf.Abs(normalizedAngles[i] - targetValue);
                if (CurrentDistance < ClosestDistance)
                {
                    ClosestDistance = CurrentDistance;
                    ClosestIndex = i;
                }
            }

            return ClosestIndex;
        }

        private float CalculateCircularMean(List<float> angles)
        {
            float sumSin = 0.0f;
            float sumCos = 0.0f;

            foreach (float angle in angles)
            {
                float Radians = angle * Mathf.Deg2Rad;
                sumSin += Mathf.Sin(Radians);
                sumCos += Mathf.Cos(Radians);
            }

            float meanRadians = Mathf.Atan2(sumSin, sumCos);

            float meanDegrees = meanRadians * Mathf.Rad2Deg;
            return NormalizeAngle(meanDegrees);
        }

        private float NormalizeAngle(float angle)
        {
            while (angle >= 360.0f) angle -= 360.0f;
            while (angle < 0.0f) angle += 360.0f;
            return angle;
        }

        private bool IsObstacleAlreadyFound(Transform obstacle, List<ObstacleHideSpots> obstacles, out ObstacleHideSpots loadedObstacleDetails, out int foundInIndex)
        {
            foundInIndex = -1;
            loadedObstacleDetails = new ObstacleHideSpots();
            for (int i = 0; i < obstacles.Count; i++)
            {
                if (obstacles[i].obstacle == obstacle)
                {
                    loadedObstacleDetails = obstacles[i];
                    foundInIndex = i;
                    return true;
                }
            }

            return false;
        }

        private RaycastHit MakeRaycastInSelectedAngle(float currentAngle, float rayLength, out Ray rayToUse, out bool isHit)
        {
            Vector3 origin = m_PlayerTransform.position + AllConfig.Instance.AIConfig.hideSpotFinderOriginOffset;

            Vector3 newForward = Quaternion.Euler(0, currentAngle, 0) * m_PlayerTransform.forward * rayLength;

            rayToUse = new Ray(origin, newForward);

            isHit = Physics.Raycast(rayToUse, out RaycastHit raycastHit, rayLength,m_PillarLayerMask);

#if UNITY_EDITOR
            DebugArrow.ForDebug(origin, newForward, Color.red);
#endif

            return raycastHit;
        }

        private void FindingPossiblePositionsAlongCurrentRay(Vector3 impactPoint, Vector3 direction, List<Vector3> possibleHideSpots)
        {
            float currentDistanceToCheckOnRay = 0f;
            float stepCount = AllConfig.Instance.AIConfig.obstacleFindingRayStartingStepCount;

            do
            {
                currentDistanceToCheckOnRay = AllConfig.Instance.AIConfig.rayTraverseStepSizeToDiscoverHidingPlace * stepCount;

                if (currentDistanceToCheckOnRay >= AllConfig.Instance.AIConfig.obstacleFindingRayMaxDistance)
                {
                    continue;
                }

                Vector3 currentPoint = impactPoint + (direction * currentDistanceToCheckOnRay);

                if (IsThisPointOutsideColliders(currentPoint) && IsThisPointNotVisibleByPlayer(currentPoint) && NavMesh.SamplePosition(currentPoint, out NavMeshHit hit, AllConfig.Instance.AIConfig.navmeshSamplePositionDistance, NavMesh.AllAreas))
                {
                    possibleHideSpots.Add(hit.position);
                    break;
                }
                else
                {
                    stepCount += AllConfig.Instance.AIConfig.obstacleFindingRayDeltaStepSize;
                }
            }
            while (currentDistanceToCheckOnRay < AllConfig.Instance.AIConfig.obstacleFindingRayMaxDistance);
        }

        private void SortHitsBasedOnDistance(List<Vector3> points)
        {
            Vector3 enemyLocation = m_EnemyTransform.position;

            points.Sort((p1, p2) => Vector3.Distance(p1, enemyLocation).CompareTo(Vector3.Distance(p2, enemyLocation)));
        }
    }
}
