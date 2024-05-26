using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyProjectilePresenter : ProjectileVisualizer
{
    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        m_Animator = GetComponent<Animator>();
        m_LineRenderer.enabled = false;
    }

    protected override void Update()
    {
        base.Update();
        m_LineRenderer.SetPosition(1, CCharacterManager.instance.playerPosition);
    }
}
