using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CShotVisualRepresenter : ProjectileVisualizer
{
    private static readonly int ANIM_PARAM_SHOW = Animator.StringToHash("shot");

    [SerializeField] private Light m_Light;
    [SerializeField] private Animator m_Animator;

    private void Start()
    {
        m_Light.enabled = false;
    }

    public override void Show()
    {
        m_Animator.SetTrigger(ANIM_PARAM_SHOW);
    }
}
