using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInteractableDetector : MonoBehaviour
{
    public RaycastHit raycastHit => m_RaycastHit;

    public bool isValidHit => m_IsValidHit;

    private RaycastHit m_RaycastHit;

    private bool m_IsValidHit;

    void Update()
    {
        Vector3 origin = transform.position;
        Vector3 localDirection = Vector3.forward;

        Vector3 worldDirection = transform.TransformDirection(localDirection);

        float rayLength = 9f;

        m_IsValidHit = Physics.Raycast(origin, worldDirection, out m_RaycastHit, rayLength);

        Debug.DrawRay(origin, worldDirection * rayLength, Color.red);
    }
}
