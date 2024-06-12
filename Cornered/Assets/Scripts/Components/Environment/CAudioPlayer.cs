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

    private void Start()
    {
        if (m_AudioClipConfigToPlay.IsPlayOnAwake)
        {
            Play(transform);
        }
    }
}
