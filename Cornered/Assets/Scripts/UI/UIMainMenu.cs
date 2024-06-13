/// <summary>
/// Filename: UIMainMenu.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CAudioPlayer))]
public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_MainPanel;
    [SerializeField] private GameObject m_HintPanel;
    [SerializeField] private GameObject m_ControlsPanel;
    [SerializeField] private List<SceneSetting> m_NewGameScenesToLoad = new List<SceneSetting>();

    private EMainMenuState m_State = EMainMenuState.Main;
    private CAudioPlayer m_AudioPlayer;

    public void OnNewGameButton()
    {
        m_AudioPlayer.Play();

        m_NewGameScenesToLoad.ForEach(x =>
        {
            SceneManager.LoadScene(x.sceneReference, x.loadSceneMode);
        });
    }

    public void OnHintButton()
    {
        m_AudioPlayer.Play();

        SetState(EMainMenuState.Hint);
    }

    public void OnHintBackButton()
    {
        BackToMain();
    }

    public void OnControlsButton()
    {
        m_AudioPlayer.Play();

        SetState(EMainMenuState.Controls);
    }

    public void OnControlsBackButton()
    {
        BackToMain();
    }

    private void BackToMain()
    {
        m_AudioPlayer.Play();

        SetState(EMainMenuState.Main);
    }

    private void Start()
    {
        m_AudioPlayer = GetComponent<CAudioPlayer>();
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
