/// <summary>
/// Filename: CMixingMachine.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CMixingMachine : MonoBehaviour
{
    private static readonly int ANIM_PARAM_CLOSE = Animator.StringToHash("close");

    [SerializeField] private Transform m_ResultTargetTransform;
    [SerializeField] private CMixingItemDetector m_MixingItemDetector;
    [SerializeField] private Animator m_TopLidAnimator;
    [SerializeField] private List<StateComponent> m_StateComponents = new();

    private EMixingMachineState m_State = EMixingMachineState.Waiting;
    private StateComponent m_CurrentStateComponent;
    private float m_ProcessStartTime;

    public void SetCurrentStateComponent(StateComponent state)
    {
        m_CurrentStateComponent = state;
    }

    public void OnMixingCallback()
    {
        IReadOnlyList<ItemDatas> detectedItems = m_MixingItemDetector.GetDetectedItems();

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

    private void OnEnable()
    {
        m_StateComponents.ForEach(x => x.Initialize(MachineButtonPressHappened, this));

        EventManager.AddListener<NewMatchStartedEvent>(OnNewMatchStarted);
    }

    private void OnDisable()
    {
        m_StateComponents.ForEach(x => x.ShutDown(MachineButtonPressHappened));

        EventManager.RemoveListener<NewMatchStartedEvent>(OnNewMatchStarted);
    }

    private void OnNewMatchStarted(NewMatchStartedEvent ev)
    {
        m_StateComponents.ForEach(x => x.ManageButtons());
    }

    private void MachineButtonPressHappened()
    {
        m_ProcessStartTime = Time.time;
        m_TopLidAnimator.SetBool(ANIM_PARAM_CLOSE, true);
        m_State = EMixingMachineState.DoingProcess;
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
            m_CurrentStateComponent.ProcessState();
            SettingDefaultState();
        }
    }

    private void SettingDefaultState()
    {
        m_State = EMixingMachineState.Waiting;
        m_CurrentStateComponent.ExitState();
        m_TopLidAnimator.SetBool(ANIM_PARAM_CLOSE, false);
    }

    private float GetCurrentProcessTime()
    {
        return AllConfig.Instance.MixingMachineConfig.GetProcessTime(m_CurrentStateComponent.ability);
    }
}

[Serializable]
public class StateComponent
{
    [SerializeField] private CButton m_StateButton;
    [SerializeField] private ParticleSystem m_StateParticleSystem;
    [SerializeField] private EAbility m_Ability;
    [SerializeField] private UnityEvent m_ItemModifyingAction;

    public EAbility ability => m_Ability;

    private CMixingMachine m_MixingMachine;

    public void ManageButtons()
    {
        m_StateButton.gameObject.SetActive(AllConfig.Instance.ProgressConfig.IsAbilityAlreadyUnlocked(ability));
    }

    public void Initialize(Action actionToCall, CMixingMachine mixingMachine)
    {
        m_StateButton.pressHappened += actionToCall;
        m_StateButton.pressHappened += EnterState;

        m_MixingMachine = mixingMachine;
    }

    public void ShutDown(Action actionToCall)
    {
        m_StateButton.pressHappened -= actionToCall;
        m_StateButton.pressHappened -= EnterState;
    }

    private void EnterState()
    {
        if (m_StateParticleSystem != null)
        {
            m_StateParticleSystem.Play();
        }

        m_MixingMachine.SetCurrentStateComponent(this);
    }

    public void ProcessState()
    {
        m_ItemModifyingAction?.Invoke();
    }
    public void ExitState()
    {
        if (m_StateParticleSystem != null)
        {
            m_StateParticleSystem.Stop();
        }
    }


}