using RotaryHeart.Lib.SerializableDictionary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Belt Config")]
public class BeltConfig : ScriptableObject
{
    [SerializeField] private CBeltElement m_BeltElementPrefab;
    [SerializeField] private float m_NormalMultiplier;
    [SerializeField] private float m_SpeedMultiplier;
    [SerializeField] private int m_DefaultCapacity;
    [SerializeField] private int m_MaxSize;
    [SerializeField] private bool m_CollectionCheck;
    [SerializeField] private SpeedAndIconDict m_SpeedAndIconDict;

    public float normalMultiplier => m_NormalMultiplier;
    public float speedMultiplier => m_SpeedMultiplier;
    public int defaultCapacity => m_DefaultCapacity;
    public int maxSize => m_MaxSize;
    public bool collectionCheck => m_CollectionCheck;

    public CBeltElement beltElementPrefab => m_BeltElementPrefab;

    public float GetCurrentMultiplier(EBeltSpeed beltSpeed)
    {
        return beltSpeed == EBeltSpeed.Normal ? m_NormalMultiplier : m_SpeedMultiplier;
    }

    public Material GetMaterialBasedOnSpeed(EBeltSpeed beltSpeed)
    {
        return m_SpeedAndIconDict[beltSpeed];
    }
}

[System.Serializable]
public class SpeedAndIconDict : SerializableDictionaryBase<EBeltSpeed, Material> { }