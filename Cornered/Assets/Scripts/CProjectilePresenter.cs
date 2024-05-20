using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CProjectilePresenter : ProjectileVisualizer
{
    private static readonly int ANIM_PARAM_SHOW = Animator.StringToHash("shot");

    private Camera m_Camera;
    private LineRenderer m_LineRenderer;
    private Animator m_Animator;

    public override void Show()
    {
        m_Animator.SetTrigger(ANIM_PARAM_SHOW);
    }

    private void Start()
    {
        m_Camera = Camera.main;
        m_LineRenderer = GetComponent<LineRenderer>();
        m_Animator = GetComponent<Animator>();
        m_LineRenderer.enabled = false;
    }

    private void Update()
    {
        m_LineRenderer.SetPosition(0, m_Camera.transform.parent.localPosition);
        m_LineRenderer.SetPosition(1, m_Camera.transform.parent.forward * 50f);
    }
}
