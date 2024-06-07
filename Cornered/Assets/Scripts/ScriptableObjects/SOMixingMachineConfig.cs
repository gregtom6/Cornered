/// <summary>
/// Filename: SOMixingMachineConfig.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using RotaryHeart.Lib.SerializableDictionary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mixing Machine Config")]
public class SOMixingMachineConfig : ScriptableObject
{
    [SerializeField] private AbilityProcessTimeDict m_AbilityProcessTimeDict;

    public float GetProcessTime(EAbility ability)
    {
        return m_AbilityProcessTimeDict[ability];
    }
}

[System.Serializable]
public class AbilityProcessTimeDict : SerializableDictionaryBase<EAbility, float> { }