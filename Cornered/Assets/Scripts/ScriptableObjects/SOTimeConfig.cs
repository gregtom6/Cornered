/// <summary>
/// Filename: SOTimeConfig.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Time Config")]
public class SOTimeConfig : ScriptableObject
{
    [SerializeField] private float m_PrepareTimeEndInSec;
    [SerializeField] private float m_WaitBetweenPreviousAndNewMatchInSec;
    [SerializeField] private float m_ReceivingHitPostProcessTime;
    [SerializeField] private float m_WaitTimeUntilGameOver;

    public float prepareTimeEndInSec => m_PrepareTimeEndInSec;

    public float waitBetweenPreviousAndNewMatchInSec => m_WaitBetweenPreviousAndNewMatchInSec;

    public float receivingHitPostProcessTime => m_ReceivingHitPostProcessTime;

    public float waitTimeUntilGameOver => m_WaitTimeUntilGameOver;
}
