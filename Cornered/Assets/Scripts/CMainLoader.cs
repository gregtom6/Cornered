/// <summary>
/// Filename: CMainLoader.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

/// <summary>
/// Input: New Input System
/// Render Pipeline: Universal RP
/// Unity: 2022.2.17f1
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class SceneSetting
{
    public string sceneReference;
    public LoadSceneMode loadSceneMode;
    public bool shouldLoad = true;
}
public class CMainLoader : MonoBehaviour
{
    [SerializeField] private List<SceneSetting> m_ScenesToLoad = new();

    private void Start()
    {
        m_ScenesToLoad.ForEach(x =>
        {
            SceneManager.LoadScene(x.sceneReference, x.loadSceneMode);
        });
    }
}
