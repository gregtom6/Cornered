using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public partial class CEnemyController : MonoBehaviour
{
    private class HideSpotFinder
    {
        private Transform m_MovementTargetPoint;
        private Transform m_EnemyTransform;
        private LayerMask m_PillarLayerMask;
        private LayerMask m_PillarPlayerLayerMask;

        public HideSpotFinder(Transform movementTargetPoint, Transform enemyTransform, LayerMask pillarPlayerLayerMask, LayerMask pillarLayerMask)
        {
            m_MovementTargetPoint = movementTargetPoint;
            m_EnemyTransform = enemyTransform;
            m_PillarLayerMask = pillarLayerMask;
            m_PillarPlayerLayerMask = pillarLayerMask;
        }

        bool IsThisPointOutsideColliders(Vector3 currentPoint)
        {
            return !HidingRoomElementsCollector.Instance.IsAnyColliderContainsPosition(currentPoint);
        }

        bool IsThisPointNotVisibleByPlayer(Vector3 currentPoint)
        {
            Vector3 direction = CCharacterManager.playerTransform.position - currentPoint;

            float distance = direction.magnitude;
            direction.Normalize();

#if UNITY_EDITOR
            DrawArrow.ForDebug(currentPoint, direction * distance, Color.red);
#endif

            if (Physics.Raycast(currentPoint, direction, out RaycastHit hit, distance, m_PillarPlayerLayerMask))
            {
                if (m_PillarLayerMask == (m_PillarLayerMask | (1 << hit.transform.gameObject.layer)))
                {
                    return true;
                }
            }

            return false;
        }

        bool ThisRayIsNotHittingPlayer(RaycastHit[] raycastHits)
        {
            return !raycastHits.Any(x => x.transform == CCharacterManager.playerTransform);
        }

        public Vector3? GetClosestHidingSpot()
        {
            float currentAngle = 0f;
            float angleRotationChecks = 5f;
            float rayLength = 30f;

            List<Vector3> possibleHideSpots = new List<Vector3>();

            do
            {
                RaycastHit[] raycastHits = MakeRaycastInSelectedAngle(currentAngle, rayLength, out Ray rayToUse);

                if (raycastHits.Length > 0 && ThisRayIsNotHittingPlayer(raycastHits))
                {
                    SortHitsBasedOnDistance(ref raycastHits);

                    FindingPossiblePositionsAlongCurrentRay(rayToUse, possibleHideSpots, raycastHits, rayLength);
                }

                currentAngle += angleRotationChecks;
            }
            while (currentAngle < 360f);

            if (possibleHideSpots.Count > 0)
            {
                possibleHideSpots.Sort((a, b) => Vector3.Distance(a, m_EnemyTransform.position).CompareTo(Vector3.Distance(b, m_EnemyTransform.position)));

                m_MovementTargetPoint.position = possibleHideSpots[0];
                return possibleHideSpots[0];
            }

            return null;
        }

        RaycastHit[] MakeRaycastInSelectedAngle(float currentAngle, float rayLength, out Ray rayToUse)
        {
            Vector3 newForward = Quaternion.Euler(0, currentAngle, 0) * m_EnemyTransform.forward * rayLength;

            rayToUse = new Ray(m_EnemyTransform.position, newForward);

            RaycastHit[] raycastHits = Physics.RaycastAll(rayToUse);

#if UNITY_EDITOR
            DrawArrow.ForDebug(m_EnemyTransform.position, newForward, Color.yellow);
#endif

            return raycastHits;
        }

        void FindingPossiblePositionsAlongCurrentRay(Ray ray, List<Vector3> possibleHideSpots, RaycastHit[] raycastHits, float rayLength)
        {
            float currentDistanceToCheckOnRay = 0f;
            float stepCount = 1f;

            do
            {
                currentDistanceToCheckOnRay = AllConfig.Instance.AIConfig.rayTraverseStepSizeToDiscoverHidingPlace * stepCount;

                if (currentDistanceToCheckOnRay > rayLength)
                {
                    continue;
                }

                Vector3 currentPoint = ray.GetPoint(currentDistanceToCheckOnRay);

                if (IsThisPointOutsideColliders(currentPoint) && IsThisPointNotVisibleByPlayer(currentPoint) && NavMesh.SamplePosition(currentPoint, out NavMeshHit hit, AllConfig.Instance.AIConfig.navmeshSamplePositionDistance, NavMesh.AllAreas))
                {
                    possibleHideSpots.Add(hit.position);
                    break;
                }
                else
                {
                    stepCount += 1f;
                }
            }
            while (currentDistanceToCheckOnRay < rayLength);
        }

        void SortHitsBasedOnDistance(ref RaycastHit[] raycastHits)
        {
            Array.Sort(raycastHits, (x, y) => x.distance.CompareTo(y.distance));
        }
    }
}
