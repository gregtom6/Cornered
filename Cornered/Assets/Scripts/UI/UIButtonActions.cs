using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonActions : MonoBehaviour
{
    public void OnResetAndRestartButtonPressed()
    {
        ProgressManager.Instance.ResetProgress();
        SceneManager.LoadScene(0);
    }

    public void OnQuitPressed()
    {
        Application.Quit();
    }
}
