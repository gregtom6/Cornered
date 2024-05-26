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
public class CLoader : MonoBehaviour
{
    [SerializeField] private List<SceneSetting> m_ScenesToLoad = new List<SceneSetting>();

    private void Start()
    {
        m_ScenesToLoad.ForEach(x =>
        {
            SceneManager.LoadScene(x.sceneReference, x.loadSceneMode);
        });
    }
}
