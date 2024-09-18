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

    private CPooledAudioSource m_ReceivedPooledAudioSource;

    public void Play()
    {
        m_ReceivedPooledAudioSource = SoundManager.instance.Play(m_AudioClipConfigToPlay);
    }

    public void Play(Transform spatialParent)
    {
        m_ReceivedPooledAudioSource = SoundManager.instance.Play(m_AudioClipConfigToPlay, spatialParent);
    }

    public void SetVolume(float volume)
    {
        if (m_ReceivedPooledAudioSource == null)
        {
            return;
        }

        SoundManager.instance.SetVolume(m_ReceivedPooledAudioSource, volume);
    }

    public void Stop()
    {
        SoundManager.instance.Stop(m_AudioClipConfigToPlay);

        m_ReceivedPooledAudioSource = null;
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
