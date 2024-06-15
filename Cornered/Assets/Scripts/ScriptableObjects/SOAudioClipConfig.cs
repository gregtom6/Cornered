/// <summary>
/// Filename: SOAudioClipConfig.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Clip Config")]
public class SOAudioClipConfig : ScriptableObject
{
    [SerializeField] private EAudioCategory m_AudioCategory = EAudioCategory.Count;
    [SerializeField] private bool m_IsLooped;
    [SerializeField] private bool m_PlayOnAwake;
    [SerializeField] private List<AudioClip> m_ClipVariations = new();

    public EAudioCategory audioCategory => m_AudioCategory;
    public bool IsLooped => m_IsLooped;
    public bool IsPlayOnAwake => m_PlayOnAwake;
    
    public AudioClip GetClip()
    {
        return m_ClipVariations.GetRandom();
    }
}
