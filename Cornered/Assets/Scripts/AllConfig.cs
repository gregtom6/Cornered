using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllConfig : MonoBehaviour
{
    [SerializeField] private CharacterConfig m_CharacterConfig;
    [SerializeField] private BeltConfig m_BeltConfig;

    public CharacterConfig CharacterConfig => m_CharacterConfig;

    public BeltConfig beltConfig => m_BeltConfig;

    public static AllConfig Instance;

    private void Awake()
    {
        Instance = this; 
    }
}
