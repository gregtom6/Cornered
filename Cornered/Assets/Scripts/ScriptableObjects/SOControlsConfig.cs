/// <summary>
/// Filename: SOControlsConfig.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Controls Config")]
public class SOControlsConfig : ScriptableObject
{
    [SerializeField] private List<ControlsPageDatas> m_ControlsPageDatas = new();

    public int controlsPageCount => m_ControlsPageDatas.Count;

    public ControlsPageDatas GetControlsPageDatas(int index)
    {
        if (index < m_ControlsPageDatas.Count)
        {
            return m_ControlsPageDatas[index];
        }

        return default;
    }
}

[Serializable]
public struct ControlsPageDatas
{
    public Sprite imageOfGameInteraction;
    public Sprite imageOfControl;
    public string text;
}