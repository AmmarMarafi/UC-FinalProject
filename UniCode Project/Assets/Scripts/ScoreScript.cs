using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreScript : MonoBehaviour
{
    public TextMeshProUGUI txtScore;
    public TextMeshProUGUI txtLabel;
    void Start()
    {
        int scoreChoice = PlayerPrefs.GetInt("show_hs", 0);

        if (scoreChoice == 0)
        {
            int kills = PlayerPrefs.GetInt("Kills", 0);
            int hits = PlayerPrefs.GetInt("Hits", 0);
            int total = PlayerPrefs.GetInt("Total", 0);
            int time = PlayerPrefs.GetInt("Time", 0);

            int score = kills + (hits / (total + 1)) + (time / 10);
            txtScore.text = score.ToString();
            txtLabel.text = "Your score is:";
        }

        else
        {
            txtScore.text = PlayerPrefs.GetInt("highscore", 0).ToString();
        }

        

        
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level");
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
