/// <summary>
/// Filename: CAudioPlayer.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAudioPlayer : MonoBehaviour
{
    [SerializeField] private SOAudioClipConfig m_AudioClipConfigToPlay;

    public void Play()
    {
        SoundManager.instance.Play(m_AudioClipConfigToPlay);
    }

    public void Play(Transform spatialParent)
    {
        SoundManager.instance.Play(m_AudioClipConfigToPlay, spatialParent);
    }

    public void Stop()
    {
        SoundManager.instance.Stop(m_AudioClipConfigToPlay);
    }

    private void Start()
    {
        if (m_AudioClipConfigToPlay.IsPlayOnAwake)
        {
            EAudioSourceType audioSourceType = SoundManager.instance.GetAudioSourceTypeBasedOnCategory(m_AudioClipConfigToPlay.audioCategory);

            if (audioSourceType == EAudioSourceType.Spatial)
            {
                Play(transform);
            }
            else
            {
                Play();
            }
        }
    }
}
