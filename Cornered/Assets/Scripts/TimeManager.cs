using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float m_StartTime;
    private bool m_IsPreparingTime;

    public static TimeManager instance;

    public float GetTimeLeft()
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

        if (GetCurrentTime() >= AllConfig.Instance.TimeConfig.prepareTimeEndInSec)
        {
            EventManager.Raise(new TimeOverHappenedEvent() { });
            m_IsPreparingTime = false;
        }
    }

    private float GetCurrentTime()
    {
        return Time.time - m_StartTime;
    }
}