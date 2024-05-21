using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AI Config")]
public class AIConfig : ScriptableObject
{
    [SerializeField] private float m_HideWhenLifeLessThanPercentage;
    [SerializeField] private float m_AttackWhenLifeMoreThanPercentage;
    [SerializeField] private float m_PreservedDistanceBetweenPlayerAndMe;

    public float hideWhenLifeLessThanPercentage => m_HideWhenLifeLessThanPercentage;

    public float preservedDistanceBetweenPlayerAndMe => m_PreservedDistanceBetweenPlayerAndMe;

    public float attackWhenLifeMoreThanPercentage => m_AttackWhenLifeMoreThanPercentage;
}
