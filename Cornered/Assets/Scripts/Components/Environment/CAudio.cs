using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CAudio : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;

    public AudioClip GetClip()
    {
        return m_AudioSource.clip;
    }

    public void Play(SOAudioClipConfig audioClipConfig)
    {
        m_AudioSource.clip = audioClipConfig.GetClip();
        m_AudioSource.loop = audioClipConfig.IsLooped;
        m_AudioSource.Play();
    }

    public void Stop()
    {
        m_AudioSource.Stop();
    }
}
