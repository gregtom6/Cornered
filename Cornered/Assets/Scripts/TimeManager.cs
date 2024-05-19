using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float m_StartTime;
    private bool m_IsPreparingTime;

    private void Start()
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

        float currentTime = Time.time - m_StartTime;
        if (currentTime >= AllConfig.Instance.TimeConfig.prepareTimeEndInSec)
        {
            EventManager.Raise(new TimeOverHappenedEvent() { });
            m_IsPreparingTime = false;
        }
    }
}