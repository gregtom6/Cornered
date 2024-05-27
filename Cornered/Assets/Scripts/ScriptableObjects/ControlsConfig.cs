using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Controls Config")]
public class ControlsConfig : ScriptableObject
{
    [SerializeField] private List<ControlsPageDatas> m_ControlsPageDatas = new();

    public int controlsPageCount => m_ControlsPageDatas.Count;

    public ControlsPageDatas GetControlsPageDatas(int index)
    {
        if (index < m_ControlsPageDatas.Count)
        {
            return m_ControlsPageDatas[index];
        }

        return default(ControlsPageDatas);
    }
}

[Serializable]
public struct ControlsPageDatas
{
    public Sprite imageOfGameInteraction;
    public Sprite imageOfControl;
    public string text;
}