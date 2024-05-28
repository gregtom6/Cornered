using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CProjectileVisualizer : MonoBehaviour
{
    protected static readonly int ANIM_PARAM_SHOW = Animator.StringToHash("shot");

    [SerializeField] protected Animator m_Animator;
    protected LineRenderer m_LineRenderer;
    protected Transform m_OriginTransform;

    public void SetOriginTransform(Transform originTransform)
    {
        m_OriginTransform = originTransform;
    }

    public void Show()
    {
        m_Animator.SetTrigger(ANIM_PARAM_SHOW);
    }

    protected virtual void Update()
    {
        if (m_OriginTransform == null)
        {
            return;
        }

        m_LineRenderer.SetPosition(0, m_OriginTransform.position);
    }
}
