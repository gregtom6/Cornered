using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CExitDoorController : HoldActivable
{
    protected static readonly int ANIM_PARAM_ACTIVATE = Animator.StringToHash("activate");

    [SerializeField] private TextMeshPro m_PercentageText;
    private Animator m_OpenAnimator;

    private float m_OpeningPercentage;

    private bool m_OpeningInProgress;

    public override void HoldProcessStarted()
    {
        m_OpeningInProgress = true;
    }

    public override void HoldProcessEnded()
    {
        m_OpeningInProgress = false;
    }

    private void Start()
    {
        m_OpenAnimator = GetComponent<Animator>();
        m_PercentageText.text = m_OpeningPercentage.ToString("F0");
    }

    private void Update()
    {
        if (!m_OpeningInProgress)
        {
            return;
        }

        m_OpeningPercentage += AllConfig.Instance.ExitDoorConfig.buttonHoldingOpenMultiplier * Time.deltaTime;

        m_OpeningPercentage = Mathf.Clamp(m_OpeningPercentage, AllConfig.Instance.ExitDoorConfig.minPercentage, AllConfig.Instance.ExitDoorConfig.maxPercentage);

        m_PercentageText.text = m_OpeningPercentage.ToString("F0");

        if (m_OpeningPercentage >= AllConfig.Instance.ExitDoorConfig.maxPercentage)
        {
            m_OpenAnimator.SetTrigger(ANIM_PARAM_ACTIVATE);
        }
    }
}
