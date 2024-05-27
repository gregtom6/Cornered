using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_MainPanel;
    [SerializeField] private GameObject m_HintPanel;
    [SerializeField] private GameObject m_ControlsPanel;
    [SerializeField] private List<SceneSetting> m_NewGameScenesToLoad = new List<SceneSetting>();

    public void OnNewGameButton()
    {
        m_NewGameScenesToLoad.ForEach(x =>
        {
            SceneManager.LoadScene(x.sceneReference, x.loadSceneMode);
        });
    }

    public void OnHintButton()
    {
        m_MainPanel.SetActive(false);
        m_HintPanel.SetActive(true);
    }

    public void OnHintBackButton()
    {
        m_MainPanel.SetActive(true);
        m_HintPanel.SetActive(false);
    }

    public void OnControlsButton()
    {
        m_MainPanel.SetActive(false);
        m_ControlsPanel.SetActive(true);
    }

    public void OnControlsBackButton()
    {
        m_MainPanel.SetActive(true);
        m_ControlsPanel.SetActive(false);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    private void Start()
    {
        m_MainPanel.SetActive(true);
        m_HintPanel.SetActive(false);
        m_ControlsPanel.SetActive(false);
    }
}
