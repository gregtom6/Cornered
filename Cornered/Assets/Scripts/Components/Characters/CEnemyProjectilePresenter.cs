/// <summary>
/// Filename: CEnemyProjectilePresenter.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Animator))]
public class CEnemyProjectilePresenter : CProjectileVisualizer
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
