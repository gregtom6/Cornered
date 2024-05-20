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

    private void Start()
    {
        m_StartTime = Time.time;
        m_IsPreparingTime = true;
        instance = this;
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