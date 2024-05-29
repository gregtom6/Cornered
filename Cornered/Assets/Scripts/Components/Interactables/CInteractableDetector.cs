/// <summary>
/// Filename: CInteractableDetector.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInteractableDetector : MonoBehaviour
{
    [SerializeField] private LayerMask m_InteractableLayerMask;

    public RaycastHit raycastHit => m_RaycastHit;

    public bool isValidHit => m_IsValidHit;

    private RaycastHit m_RaycastHit;

    private bool m_IsValidHit;

    private void Update()
    {
        Vector3 origin = transform.position;
        Vector3 localDirection = Vector3.forward;

        Vector3 worldDirection = transform.TransformDirection(localDirection);

        float rayLength = 9f;

        m_IsValidHit = Physics.Raycast(origin, worldDirection, out m_RaycastHit, rayLength, m_InteractableLayerMask);
    }
}
