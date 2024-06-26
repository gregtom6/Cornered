/// <summary>
/// Filename: CPicker.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(CInteractableDetector))]
public class CPicker : MonoBehaviour
{
    [SerializeField] private GameInput m_GameInput;

    public IPickable pickedPickable => m_PickedPickable;

    private CInteractableDetector m_InteractableDetector;

    private IPickable m_PickedPickable;

    public void RemovePickable()
    {
        m_PickedPickable = null;
    }

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
        if (m_PickedPickable == null)
        {
            if (m_InteractableDetector != null && m_InteractableDetector.isValidHit)
            {
                RaycastHit raycastHit = m_InteractableDetector.raycastHit;

                IPickable pickable = raycastHit.collider.GetComponentInParent<IPickable>();

                if (pickable != null)
                {
                    pickable.Pickup(transform);
                    m_PickedPickable = pickable;
                }
            }
        }
        else
        {
            if (m_PickedPickable.IsPicked())
            {
                m_PickedPickable.Drop();
                m_PickedPickable = null;
            }
        }
    }
}
