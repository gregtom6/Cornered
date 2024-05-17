using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CharacterConfig")]
public class CharacterConfig : ScriptableObject
{
    [SerializeField] float m_RunSpeed;
    [SerializeField] float m_HeadMaxRotX;
    [SerializeField] float m_HeadMinRotX;
    [SerializeField] float m_HeadRotSpeed;

    public float runSpeed => m_RunSpeed;
    public float headMaxRotX => m_HeadMaxRotX;
    public float headMinRotX => m_HeadMinRotX;
    public float headRotSpeed => m_HeadRotSpeed;
}
