/// <summary>
/// Filename: UIButtonActions.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CAudioPlayer))]
public class UIButtonActions : MonoBehaviour
{
    private CAudioPlayer m_AudioPlayer;

    public void OnResetAndRestartButtonPressed()
    {
        m_AudioPlayer.Play();

        ProgressManager.Instance.ResetProgress();
        SceneManager.LoadScene(0);
    }

    public void OnQuitPressed()
    {
        m_AudioPlayer.Play();

        Application.Quit();
    }

    private void Start()
    {
        m_AudioPlayer= GetComponent<CAudioPlayer>();
    }
}
