using RotaryHeart.Lib.SerializableDictionary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Clip Container Config")]
public class SOAudioConfig : ScriptableObject
{
    [SerializeField] private AudioSourcePrefabDict m_AudioSourcePrefabDict;
    [SerializeField] private int m_DefaultCapacity;
    [SerializeField] private int m_MaxSize;
    [SerializeField] private bool m_CollectionCheck;
    [SerializeField] private List<SOAudioClipConfig> m_AudioClips = new();

    public int defaultCapacity => m_DefaultCapacity;
    public int maxSize => m_MaxSize;
    public bool collectionCheck => m_CollectionCheck;

    public CPooledAudioSource GetAudioSourcePrefab(EAudioSourceType audioSourceType)
    {
        return m_AudioSourcePrefabDict[audioSourceType];
    }
}

[System.Serializable]
public class AudioSourcePrefabDict : SerializableDictionaryBase<EAudioSourceType, CPooledAudioSource> { }