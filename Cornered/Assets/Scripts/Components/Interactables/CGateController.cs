using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGateController : MonoBehaviour
{
    private static readonly int ANIM_PARAM_OPEN = Animator.StringToHash("open");

    [SerializeField] private Animator m_GateAnimator;
    [SerializeField] private CGateEnterDetector m_GateEnterDetector;
    [SerializeField] private CGateEnterDetector m_GateExitDetector;

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
