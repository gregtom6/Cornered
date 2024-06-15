/// <summary>
/// Filename: CStepProcesser.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStepProcesser : MonoBehaviour
{
    [SerializeField] private CAudioPlayer m_StepAudioPlayer;

    private void OnEnable()
    {
        EventManager.AddListener<StepHappenedEvent>(OnStepHappenedEvent);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<StepHappenedEvent>(OnStepHappenedEvent);
    }

    private void OnStepHappenedEvent(StepHappenedEvent ev)
    {
        if (IsThisTransformGeneratedStep(ev))
        {
            m_StepAudioPlayer.Play();
        }
    }

    private bool IsThisTransformGeneratedStep(StepHappenedEvent ev)
    {
        return ev.animEventCatcherTransform == transform;
    }
}
