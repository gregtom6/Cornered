using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInteractor : MonoBehaviour
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
            m_GameInput.LeftPointerDown += OnLeftPointerDown;
        }
    }


    private void OnDisable()
    {
        if (m_GameInput != null)
        {
            m_GameInput.LeftPointerDown -= OnLeftPointerDown;
        }
    }
    private void OnLeftPointerDown(Vector2 obj)
    {
        if (m_InteractableDetector != null && m_InteractableDetector.isValidHit)
        {
            RaycastHit raycastHit = m_InteractableDetector.raycastHit;

            IInteractable interactable = raycastHit.collider.GetComponentInParent<IInteractable>();

            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
}
