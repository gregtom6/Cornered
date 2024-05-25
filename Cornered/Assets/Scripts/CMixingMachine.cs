using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CMixingMachine : MonoBehaviour
{
    protected static readonly int ANIM_PARAM_CLOSE = Animator.StringToHash("close");

    [SerializeField] private Transform m_ResultTargetTransform;
    [SerializeField] private CMixingItemDetector m_MixingItemDetector;
    [SerializeField] private CButton m_FreezeButton;
    [SerializeField] private CButton m_BurnButton;
    [SerializeField] private CButton m_ConvertButton;
    [SerializeField] private ParticleSystem m_BurnParticleSystem;
    [SerializeField] private ParticleSystem m_FreezeParticleSystem;
    [SerializeField] private Animator m_TopLidAnimator;

    private EMixingMachineState m_State = EMixingMachineState.Waiting;
    private float m_ProcessStartTime;

    private void OnEnable()
    {
        m_FreezeButton.pressHappened += FreezeButtonPressed;
        m_BurnButton.pressHappened += BurnButtonPressed;
        m_ConvertButton.pressHappened += ConvertButtonPressed;

        EventManager.AddListener<NewMatchStartedEvent>(OnNewMatchStarted);
    }

    private void OnDisable()
    {
        m_FreezeButton.pressHappened -= FreezeButtonPressed;
        m_BurnButton.pressHappened -= BurnButtonPressed;
        m_ConvertButton.pressHappened -= ConvertButtonPressed;

        EventManager.RemoveListener<NewMatchStartedEvent>(OnNewMatchStarted);
    }

    private void OnNewMatchStarted(NewMatchStartedEvent ev)
    {
        ActivateUnlockedButtons();
    }

    private void ActivateUnlockedButtons()
    {
        m_FreezeButton.gameObject.SetActive(AllConfig.Instance.ProgressConfig.IsAbilityAlreadyUnlocked(EAbility.Freeze));
        m_BurnButton.gameObject.SetActive(AllConfig.Instance.ProgressConfig.IsAbilityAlreadyUnlocked(EAbility.Burn));
    }

    private void FreezeButtonPressed()
    {
        m_FreezeParticleSystem.Play();
        m_State = EMixingMachineState.Freezing;
        m_ProcessStartTime = Time.time;
        m_TopLidAnimator.SetBool(ANIM_PARAM_CLOSE, true);
    }

    private void BurnButtonPressed()
    {
        m_BurnParticleSystem.Play();
        m_State = EMixingMachineState.Heating;
        m_ProcessStartTime = Time.time;
        m_TopLidAnimator.SetBool(ANIM_PARAM_CLOSE, true);
    }

    private void ConvertButtonPressed()
    {
        m_State = EMixingMachineState.Mixing;
        m_ProcessStartTime = Time.time;
        m_TopLidAnimator.SetBool(ANIM_PARAM_CLOSE, true);
    }

    private void Mixing()
    {
        IReadOnlyList<ItemTypes> detectedItems = m_MixingItemDetector.GetDetectedItems();

        if (detectedItems.Count == 0 || detectedItems.Any(x => x.item == EItemType.EmptyItem))
        {
            return;
        }

        GameObject resultPrefab = AllConfig.Instance.RecipeConfig.GetResultItem(detectedItems);

        if (resultPrefab != null)
        {
            Instantiate(resultPrefab, m_ResultTargetTransform.position, Quaternion.identity, null);
            m_MixingItemDetector.DestroyAllItems();
        }
    }

    private void Update()
    {
        if (m_State == EMixingMachineState.Waiting)
        {
            return;
        }

        float currentTime = Time.time - m_ProcessStartTime;
        if (currentTime >= GetCurrentProcessTime())
        {
            DoProcessSteps();
            SettingDefaultState();
        }

    }

    private void SettingDefaultState()
    {
        m_State = EMixingMachineState.Waiting;
        m_BurnParticleSystem.Stop();
        m_FreezeParticleSystem.Stop();
        m_TopLidAnimator.SetBool(ANIM_PARAM_CLOSE, false);
    }

    private float GetCurrentProcessTime()
    {
        switch (m_State)
        {
            case EMixingMachineState.Freezing:
                return AllConfig.Instance.MixingMachineConfig.freezingTime;
            case EMixingMachineState.Heating:
                return AllConfig.Instance.MixingMachineConfig.burningTime;
            case EMixingMachineState.Mixing:
                return AllConfig.Instance.MixingMachineConfig.mixingTime;
        }

        return 0f;
    }

    private void DoProcessSteps()
    {
        switch (m_State)
        {
            case EMixingMachineState.Freezing:
                m_MixingItemDetector.FreezeAllItems();
                break;
            case EMixingMachineState.Heating:
                m_MixingItemDetector.BurnAllItems();
                break;
            case EMixingMachineState.Mixing:
                Mixing();
                break;
        }
    }
}


public enum EMixingMachineState
{
    Heating,
    Freezing,
    Mixing,
    Waiting,

    Count,
}