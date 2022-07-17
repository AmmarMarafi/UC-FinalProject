using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void ShowHighScore()
    {
        PlayerPrefs.SetInt("show_hs", 1);
        SceneManager.LoadScene("ScoreScene");
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
