/// <summary>
/// Filename: CInteractor.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInteractor : MonoBehaviour
{
    [SerializeField] private GameInput m_GameInput;

    private CInteractableDetector m_InteractableDetector;

    private IHoldable m_Holdable;

    private void Start()
    {
        m_InteractableDetector = GetComponent<CInteractableDetector>();
    }

    private void OnEnable()
    {
        if (m_GameInput != null)
        {
            m_GameInput.LeftPointerDown += OnLeftPointerDown;
            m_GameInput.LeftPointerUp += OnLeftPointerUp;
        }
    }

    private void OnDisable()
    {
        if (m_GameInput != null)
        {
            m_GameInput.LeftPointerDown -= OnLeftPointerDown;
            m_GameInput.LeftPointerUp -= OnLeftPointerUp;
        }
    }

    private void Update()
    {
        if (m_Holdable == null)
        {
            return;
        }

        if (m_InteractableDetector != null && !m_InteractableDetector.isValidHit)
        {
            m_Holdable.HoldingFinished();
            m_Holdable = null;
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

            m_Holdable = raycastHit.collider.GetComponentInParent<IHoldable>();

            if (m_Holdable != null)
            {
                m_Holdable.HoldingStarted();
            }
        }
    }

    private void OnLeftPointerUp(Vector2 obj)
    {
        if (m_InteractableDetector != null && m_InteractableDetector.isValidHit)
        {
            RaycastHit raycastHit = m_InteractableDetector.raycastHit;

            m_Holdable = raycastHit.collider.GetComponentInParent<IHoldable>();

            if (m_Holdable != null)
            {
                m_Holdable.HoldingFinished();
            }
        }
    }
}
