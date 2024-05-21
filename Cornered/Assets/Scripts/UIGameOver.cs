using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    public void RestartButtonClick()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitButtonClick()
    {
        Application.Quit();
    }
}
