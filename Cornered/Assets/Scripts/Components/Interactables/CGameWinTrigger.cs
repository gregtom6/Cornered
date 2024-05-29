/// <summary>
/// Filename: CGameWinTrigger.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CGameWinTrigger : MonoBehaviour
{
    [SerializeField] private List<SceneSetting> m_ScenesToLoad = new List<SceneSetting>();

    private void OnTriggerEnter(Collider other)
    {
        m_ScenesToLoad.ForEach(x =>
        {
            SceneManager.LoadScene(x.sceneReference, x.loadSceneMode);
        });
    }
}
