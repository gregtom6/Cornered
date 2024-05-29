/// <summary>
/// Filename: UIMainMenu.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

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

    private EMainMenuState m_State = EMainMenuState.Main;

    public void OnNewGameButton()
    {
        m_NewGameScenesToLoad.ForEach(x =>
        {
            SceneManager.LoadScene(x.sceneReference, x.loadSceneMode);
        });
    }

    public void OnHintButton()
    {
        SetState(EMainMenuState.Hint);
    }

    public void OnHintBackButton()
    {
        SetState(EMainMenuState.Main);
    }

    public void OnControlsButton()
    {
        SetState(EMainMenuState.Controls);
    }

    public void OnControlsBackButton()
    {
        SetState(EMainMenuState.Main);
    }

    private void Start()
    {
        ShowState();
    }

    private void SetState(EMainMenuState state)
    {
        m_State = state;
        ShowState();
    }

    private void ShowState()
    {
        m_MainPanel.SetActive(m_State == EMainMenuState.Main);
        m_HintPanel.SetActive(m_State == EMainMenuState.Hint);
        m_ControlsPanel.SetActive(m_State == EMainMenuState.Controls);
    }
}
