/// <summary>
/// Filename: CPooledAudioSource.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CPooledAudioSource : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;

    public EAudioCategory audioCategory => m_AudioCategory;
    public Action<CPooledAudioSource> onAudioFinished;
    private EAudioCategory m_AudioCategory = EAudioCategory.Count;
    private bool m_PrevPlayState;

    public AudioClip GetClip()
    {
        return m_AudioSource.clip;
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent, false);
    }

    public void Play(SOAudioClipConfig audioClipConfig)
    {
        m_AudioSource.clip = audioClipConfig.GetClip();
        m_AudioSource.loop = audioClipConfig.IsLooped;
        m_AudioSource.playOnAwake = audioClipConfig.IsPlayOnAwake;
        m_AudioSource.Play();

        m_AudioCategory = audioClipConfig.audioCategory;
    }

    public void Stop()
    {
        m_AudioSource.Stop();
    }

    private void Update()
    {
        if (!m_PrevPlayState && !m_AudioSource.isPlaying)
        {
            return;
        }

        if (!m_PrevPlayState && m_AudioSource.isPlaying)
        {
            m_PrevPlayState = true;
        }

        if (m_PrevPlayState && !m_AudioSource.isPlaying)
        {
            onAudioFinished?.Invoke(this);
            m_PrevPlayState = false;
        }
    }
}
