/// <summary>
/// Filename: CGateController.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CAudioPlayer))]
public class CGateController : MonoBehaviour
{
    private static readonly int ANIM_PARAM_OPEN = Animator.StringToHash("open");

    [SerializeField] private Animator m_GateAnimator;
    [SerializeField] private CGateEnterDetector m_GateEnterDetector;
    [SerializeField] private CGateEnterDetector m_GateExitDetector;

    private CAudioPlayer m_AudioPlayer;

    public void OnGateMovementStarted()
    {
        m_AudioPlayer.Play(transform);
    }

    public void OnGateMovementFinished()
    {
        m_AudioPlayer.Stop();
    }

    private void Start()
    {
        m_AudioPlayer = GetComponent<CAudioPlayer>();
    }

    private void OnEnable()
    {
        m_GateEnterDetector.enterHappened += OnEnterHappened;
        m_GateExitDetector.enterHappened += OnExitHappened;
    }

    private void OnDisable()
    {
        m_GateEnterDetector.enterHappened -= OnEnterHappened;
        m_GateExitDetector.enterHappened -= OnExitHappened;
    }

    private void OnEnterHappened()
    {
        m_GateAnimator.SetBool(ANIM_PARAM_OPEN, true);
    }

    private void OnExitHappened()
    {
        m_GateAnimator.SetBool(ANIM_PARAM_OPEN, false);
    }
}
