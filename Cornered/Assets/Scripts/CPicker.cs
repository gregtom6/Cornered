using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPicker : MonoBehaviour
{
    [SerializeField] private GameInput m_GameInput;

    private CInteractableDetector m_InteractableDetector;

    private void Start()
    {
        m_InteractableDetector = GetComponent<CInteractableDetector>();
    }

    private void OnEnable()
    {
        if (m_GameInput != null)
        {
            m_GameInput.RightPointerDown += OnRightPointerDown;
        }
    }


    private void OnDisable()
    {
        if (m_GameInput != null)
        {
            m_GameInput.RightPointerDown -= OnRightPointerDown;
        }
    }
    private void OnRightPointerDown(Vector2 obj)
    {
        if (m_InteractableDetector != null && m_InteractableDetector.isValidHit)
        {
            RaycastHit raycastHit = m_InteractableDetector.raycastHit;

            IPickable pickable = raycastHit.collider.GetComponentInParent<IPickable>();

            if (pickable != null)
            {
                if (pickable.IsPicked())
                {
                    pickable.Drop();
                }
                else
                {
                    pickable.Pickup();
                }
            }
        }
    }
}
