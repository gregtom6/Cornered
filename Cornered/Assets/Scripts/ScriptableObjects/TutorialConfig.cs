using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tutorial Config")]
public class TutorialConfig : ScriptableObject
{
    [SerializeField] private List<TutorialPageDatas> m_TutorialPageDatas = new();

    public int tutorialPageCount => m_TutorialPageDatas.Count;

    public TutorialPageDatas GetTutorialPageDatas(int index)
    {
        if (index < m_TutorialPageDatas.Count)
        {
            return m_TutorialPageDatas[index];
        }

        return default(TutorialPageDatas);
    }
}

[Serializable]
public struct TutorialPageDatas
{
    public Sprite image;
    public string text;
}