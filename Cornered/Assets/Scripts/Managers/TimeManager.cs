/// <summary>
/// Filename: TimeManager.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float m_StartTime;
    private float m_PreparingTimeLeft;
    private bool m_IsPreparingTime;

    public static TimeManager instance;

    public float preparingTimeLeft => m_PreparingTimeLeft;

    public void ZeroingTimer()
    {
        PreparingTimeEnded();
    }

    private float GetTimeLeft()
    {
        return Mathf.Max(AllConfig.Instance.TimeConfig.prepareTimeEndInSec - GetCurrentTime(), 0f);
    }

    private void OnEnable()
    {
        EventManager.AddListener<NewMatchStartedEvent>(OnNewMatchStartedEvent);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<NewMatchStartedEvent>(OnNewMatchStartedEvent);
    }

    private void Start()
    {
        instance = this;
    }

    private void OnNewMatchStartedEvent(NewMatchStartedEvent ev)
    {
        m_StartTime = Time.time;
        m_IsPreparingTime = true;
    }

    private void Update()
    {
        if (!m_IsPreparingTime)
        {
            return;
        }

        m_PreparingTimeLeft = GetTimeLeft();

        if (GetCurrentTime() >= AllConfig.Instance.TimeConfig.prepareTimeEndInSec)
        {
            PreparingTimeEnded();
        }
    }

    private void PreparingTimeEnded()
    {
        m_IsPreparingTime = false;
        m_PreparingTimeLeft = 0;
        EventManager.Raise(new TimeOverHappenedEvent() { });
    }

    private float GetCurrentTime()
    {
        return Time.time - m_StartTime;
    }
}