using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        EventManager.AddListener<CharacterDefeatedEvent>(OnCharacterDefeatedEvent);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<CharacterDefeatedEvent>(OnCharacterDefeatedEvent);
    }

    private void OnCharacterDefeatedEvent(CharacterDefeatedEvent ev)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
