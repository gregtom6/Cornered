using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Time Config")]
public class TimeConfig : ScriptableObject
{
    [SerializeField] private float m_PrepareTimeEndInSec;

    public float prepareTimeEndInSec => m_PrepareTimeEndInSec;
}
