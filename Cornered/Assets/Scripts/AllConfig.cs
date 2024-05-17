using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllConfig : MonoBehaviour
{
    [SerializeField] private CharacterConfig m_CharacterConfig;

    public CharacterConfig CharacterConfig => m_CharacterConfig;

    public static AllConfig Instance;

    private void Start()
    {
        Instance = this; 
    }
}
