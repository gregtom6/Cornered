using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileVisualizer : MonoBehaviour
{
    protected static readonly int ANIM_PARAM_SHOW = Animator.StringToHash("shot");

    [SerializeField] protected Animator m_Animator;
    protected LineRenderer m_LineRenderer;

    public void Show()
    {
        m_Animator.SetTrigger(ANIM_PARAM_SHOW);
    }

    protected void Start()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        m_Animator = GetComponent<Animator>();
        m_LineRenderer.enabled = false;
    }
}
