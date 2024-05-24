using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mixing Machine Config")]
public class MixingMachineConfig : ScriptableObject
{
    [SerializeField] private float m_FreezingTime;
    [SerializeField] private float m_BurningTime;
    [SerializeField] private float m_MixingTime;

    public float freezingTime => m_FreezingTime;
    public float burningTime => m_BurningTime;
    public float mixingTime => m_MixingTime;
}
