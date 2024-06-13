/// <summary>
/// Filename: SoundManager.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class SoundManager : MonoBehaviour
{
    private List<CPooledAudioSource> m_AllAudios = new();
    private List<CPooledAudioSource> m_ActiveAudios = new();
    private Dictionary<EAudioCategory, IObjectPool<CPooledAudioSource>> m_AudioSources = new();

    public static SoundManager instance;

    public void Play(SOAudioClipConfig audioClipConfig)
    {
        CPooledAudioSource audioSource = m_AudioSources[audioClipConfig.audioCategory].Get();
        audioSource.Play(audioClipConfig);
        m_ActiveAudios.Add(audioSource);

    }

    public void Play(SOAudioClipConfig audioClipConfig, Transform spatialParent)
    {
        CPooledAudioSource audioSource = m_AudioSources[audioClipConfig.audioCategory].Get();

        if (audioSource != null)
        {
            audioSource.Play(audioClipConfig);
            m_ActiveAudios.Add(audioSource);
            audioSource.SetParent(spatialParent);
        }
    }

    public void Stop(SOAudioClipConfig audioClipConfig)
    {
        CPooledAudioSource audio = m_ActiveAudios.Where(x => x.GetClip() == audioClipConfig.GetClip()).FirstOrDefault();

        if (audio != null)
        {
            audio.Stop();
            m_ActiveAudios.Remove(audio);
            m_AudioSources[audioClipConfig.audioCategory].Release(audio);
        }
    }

    public void StopAll()
    {
        m_ActiveAudios.ForEach(x =>
        {
            x.Stop();

            if (x.gameObject.activeInHierarchy)
            {
                m_AudioSources[x.audioCategory].Release(x);
            }
        });

        m_ActiveAudios.Clear();
    }

    public EAudioSourceType GetAudioSourceTypeBasedOnCategory(EAudioCategory category)
    {
        if (category == EAudioCategory.GameplaySFX)
        {
            return EAudioSourceType.Spatial;
        }

        return EAudioSourceType.Global;
    }

    private void Start()
    {
        for (EAudioCategory category = 0; category < EAudioCategory.Count; category++)
        {
            EAudioSourceType audioSourceType = GetAudioSourceTypeBasedOnCategory(category);

            m_AudioSources.Add(category, new ObjectPool<CPooledAudioSource>(GetFuncBasedOnAudioSourceType(audioSourceType), OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
            AllConfig.Instance.AudioConfig.collectionCheck, AllConfig.Instance.AudioConfig.defaultCapacity, AllConfig.Instance.AudioConfig.maxSize));
        }
    }

    private void OnAudioFinished(CPooledAudioSource audioSource)
    {
        m_AudioSources[audioSource.audioCategory].Release(audioSource);
    }

    private void OnReleaseToPool(CPooledAudioSource element)
    {
        if (element != null)
        {
            element.gameObject.SetActive(false);
        }
    }

    private void OnGetFromPool(CPooledAudioSource element)
    {
        if (element != null)
        {
            element.gameObject.SetActive(true);
        }
    }

    private void OnDestroyPooledObject(CPooledAudioSource element)
    {
        Destroy(element.gameObject);
    }

    Func<CPooledAudioSource> GetFuncBasedOnAudioSourceType(EAudioSourceType audioSourceType)
    {
        if (audioSourceType == EAudioSourceType.Global)
        {
            return CreateGlobalAudioSource;
        }

        return CreateSpatialAudioSource;
    }

    private CPooledAudioSource CreateGlobalAudioSource()
    {
        return CreateAudioSource(EAudioSourceType.Global);
    }

    private CPooledAudioSource CreateSpatialAudioSource()
    {
        return CreateAudioSource(EAudioSourceType.Spatial);
    }

    private CPooledAudioSource CreateAudioSource(EAudioSourceType audioSourceType)
    {
        CPooledAudioSource audioSource = Instantiate<CPooledAudioSource>(AllConfig.Instance.AudioConfig.GetAudioSourcePrefab(audioSourceType), Vector3.zero, Quaternion.identity, transform);
        audioSource.onAudioFinished += OnAudioFinished;
        m_AllAudios.Add(audioSource);

        return audioSource;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        m_AllAudios.ForEach(x => x.onAudioFinished -= OnAudioFinished);
    }
}