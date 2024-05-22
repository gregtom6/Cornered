using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerProjectilePresenter : ProjectileVisualizer
{
    [SerializeField] protected Transform m_HeadTransform;
    private void Update()
    {
        m_LineRenderer.SetPosition(0, m_HeadTransform.position);

        m_LineRenderer.SetPosition(1, m_HeadTransform.position+m_HeadTransform.forward*100f);
    }
}
