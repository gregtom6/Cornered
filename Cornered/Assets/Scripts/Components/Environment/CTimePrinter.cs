/// <summary>
/// Filename: CTimePrinter.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CTimePrinter : MonoBehaviour
{
    [SerializeField] private TextMeshPro m_TimeText;

    private void Update()
    {
        m_TimeText.text = TimeManager.instance.preparingTimeLeft.ToString("F0");
    }
}
