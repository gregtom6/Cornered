using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

[RequireComponent(typeof(Volume))]
public class CPostProcessController : MonoBehaviour
{
    private bool m_PlayerReceivedShot;
    private UniversalRenderPipelineAsset m_UrpAsset;
    private Volume m_Volume;
    private float m_TimeWhenPostProcessStarted;

    private void Start()
    {
        m_UrpAsset = (UniversalRenderPipelineAsset)GraphicsSettings.currentRenderPipeline;
        m_Volume = GetComponent<Volume>();

        SetShotEffect(0f);
    }

    private void OnEnable()
    {
        EventManager.AddListener<CharacterReceivedShotEvent>(OnCharacterReceivedShot);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<CharacterReceivedShotEvent>(OnCharacterReceivedShot);
    }

    private void OnCharacterReceivedShot(CharacterReceivedShotEvent characterReceivedShot)
    {
        if (characterReceivedShot.charType == ECharacterType.Enemy)
        {
            return;
        }

        m_PlayerReceivedShot = true;

        m_TimeWhenPostProcessStarted = Time.time;

        SetShotEffect(1f);
    }

    private void SetShotEffect(float alpha)
    {
        if (m_UrpAsset != null)
        {
            m_UrpAsset.renderScale = 1f - alpha;
        }

        m_Volume.weight = alpha;
    }

    private void Update()
    {
        if (!m_PlayerReceivedShot)
        {
            return;
        }

        float currentTime = Time.time - m_TimeWhenPostProcessStarted;
        float percentage = currentTime / AllConfig.Instance.TimeConfig.receivingHitPostProcessTime;
        percentage = Mathf.Clamp01(percentage);

        SetShotEffect(1f - percentage);

        if (percentage >= 1f)
        {
            m_PlayerReceivedShot = false;
        }
    }
}
