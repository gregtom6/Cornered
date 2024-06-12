/// <summary>
/// Filename: SoundManager.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class SoundManager : MonoBehaviour
{
    private List<CAudio> m_ActiveAudios = new();
    private Dictionary<EAudioCategory, IObjectPool<CAudio>> m_AudioSources = new();

    public static SoundManager instance;

    public void Play(SOAudioClipConfig audioClipConfig)
    {
        CAudio audioSource = m_AudioSources[audioClipConfig.audioCategory].Get();
        audioSource.Play(audioClipConfig);
    }

    public void Play(SOAudioClipConfig audioClipConfig, Vector3 position)
    {

    }

    public void Stop(SOAudioClipConfig audioClipConfig)
    {
        CAudio audio = m_ActiveAudios.Where(x => x.GetClip() == audioClipConfig.GetClip()).FirstOrDefault();
        audio.Stop();
    }

    public void StopAll()
    {
        for (EAudioCategory category = 0; category < EAudioCategory.Count; category++)
        {
            m_AudioSources[category].Clear();
        }

        m_ActiveAudios.ForEach(x => x.Stop());
        m_ActiveAudios.Clear();
    }

    private void Start()
    {
        for (EAudioCategory category = 0; category < EAudioCategory.Count; category++)
        {
            EAudioSourceType audioSourceType = GetAudioSourceTypeBasedOnCategory(category);
            GameObject audioSourcePrefab = AllConfig.Instance.AudioConfig.GetAudioSourcePrefab(audioSourceType);

            m_AudioSources[category] = new ObjectPool<CAudio>(CreateGlobalAudioSource, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
            AllConfig.Instance.AudioConfig.collectionCheck, AllConfig.Instance.AudioConfig.defaultCapacity, AllConfig.Instance.AudioConfig.maxSize);
        }
    }

    private EAudioSourceType GetAudioSourceTypeBasedOnCategory(EAudioCategory category)
    {
        if (category == EAudioCategory.GameplaySFX)
        {
            return EAudioSourceType.Spatial;
        }

        return EAudioSourceType.Global;
    }

    private void OnReleaseToPool(CAudio element)
    {
        element.gameObject.SetActive(false);
    }

    private void OnGetFromPool(CAudio element)
    {
        //element.transform.position = m_SpawnPoint.position;
        element.gameObject.SetActive(true);
    }

    private void OnDestroyPooledObject(CAudio element)
    {
        Destroy(element.gameObject);
    }

    private CAudio CreateGlobalAudioSource()
    {
        CAudio audioSource = Instantiate<CAudio>(null, transform.position, Quaternion.identity, transform);

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
}