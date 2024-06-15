/// <summary>
/// Filename: CEquipper.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CPicker))]
[RequireComponent(typeof(CAudioPlayer))]
public class CEquipper : MonoBehaviour
{
    [SerializeField] private GameInput m_GameInput;

    private CPicker m_Picker;
    private CAudioPlayer m_AudioPlayer;

    private void Start()
    {
        m_Picker = GetComponent<CPicker>();
        m_AudioPlayer = GetComponent<CAudioPlayer>();
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
        if (m_Picker != null)
        {
            if (m_Picker.pickedPickable != null)
            {
                IEquippable equippable = m_Picker.pickedPickable.GetEquippable();

                if (equippable != null)
                {
                    m_Picker.RemovePickable();
                    equippable.Equip();

                    m_AudioPlayer.Play();
                }
            }
        }
    }
}
