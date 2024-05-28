using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "GameInput", menuName = "GameInput")]
public class GameInput : ScriptableObject, GameInputActions.IGameplayActions
{
    public Vector2 mousePosScreen => m_MousePosScreen;

    public event Action<Vector2> PointerPosition = delegate { };
    public event Action<Vector2> PointerDelta = delegate { };
    public event Action<float> ForwardBackward = delegate { };
    public event Action<float> LeftRight = delegate { };
    public event Action<Vector2> LeftPointerDown = delegate { };
    public event Action<Vector2> LeftPointerUp = delegate { };
    public event Action<Vector2> RightPointerDown = delegate { };
    public event Action<Vector2> RightPointerUp = delegate { };

    private GameInputActions m_InputActions = null;
    private Vector2 m_MousePosScreen;

    public void OnForwardBackwardMovement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ForwardBackward?.Invoke(context.ReadValue<float>());
        }
        else if (context.canceled)
        {
            ForwardBackward?.Invoke(0f);
        }
    }

    public void OnLeftRightMovement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            LeftRight?.Invoke(context.ReadValue<float>());
        }
        else if (context.canceled)
        {
            LeftRight?.Invoke(0f);
        }
    }

    private void EnableGameplayInput()
    {
        m_InputActions.Gameplay.Enable();
    }

    private void DisableGameplayInput()
    {
        m_InputActions.Gameplay.Disable();
    }

    private void OnEnable()
    {
        if (m_InputActions == null)
        {
            m_InputActions = new GameInputActions();
            m_InputActions.Gameplay.SetCallbacks(this);
        }

        EnableGameplayInput();
    }

    private void OnDisable()
    {
        DisableGameplayInput();
    }

    public void OnPointerPosition(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            m_MousePosScreen = context.ReadValue<Vector2>();

            PointerPosition?.Invoke(m_MousePosScreen);
        }
    }

    public void OnPointerDelta(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 delta = context.ReadValue<Vector2>();

            PointerDelta?.Invoke(delta);
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                LeftPointerDown?.Invoke(Mouse.current.position.ReadValue());
                break;

            case InputActionPhase.Canceled:
                LeftPointerUp?.Invoke(Mouse.current.position.ReadValue());
                break;
            default:
                break;
        }
    }

    public void OnPickupDrop(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                RightPointerDown?.Invoke(Mouse.current.position.ReadValue());
                break;

            case InputActionPhase.Canceled:
                RightPointerUp?.Invoke(Mouse.current.position.ReadValue());
                break;
            default:
                break;
        }
    }
}
