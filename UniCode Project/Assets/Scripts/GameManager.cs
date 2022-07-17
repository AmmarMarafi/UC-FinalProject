using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



public class GameManager : MonoBehaviour
{

    public GameObject BotPrefab;
    public float nextSpawn;
    public GameObject [] spawnPoints;
    public GameObject player;
    public TextMeshProUGUI txtKills;
    public TextMeshProUGUI txtTime;
    public int kills;
    public int totalBullets;
    public int hitBullets;
    public bool gamePaused = false;
    public bool gameSlowed = false;
    public bool gameBoost = false;


    
    void Start()
    {
        nextSpawn = 5f;
    }

    // Update is called once per frame
    void Update()
    {

        FixSpeed();

        txtKills.text = kills.ToString();
        txtTime.text = ((int)Time.timeSinceLevelLoad).ToString();

        if (Time.timeSinceLevelLoad > nextSpawn)
        {
            Vector3 newPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            GameObject newBot = Instantiate(BotPrefab);
            newBot.transform.position = newPosition;
            nextSpawn = Time.timeSinceLevelLoad + 5f;

            newBot.GetComponent<BotScript>().targetObject = player;
            newBot.GetComponent<BotScript>().gameManager = this;
        }


    }
    void FixSpeed()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gamePaused)
                Time.timeScale = 1f;
            else
                Time.timeScale = 0f;
            gamePaused = !gamePaused;
            gameSlowed = false;
            gameBoost = false;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (gameSlowed)
                Time.timeScale = 1f;
            else
                Time.timeScale = 0.5f;
            gameSlowed = !gameSlowed;
            gamePaused = false;
            gameBoost = false;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (gameBoost)
                Time.timeScale = 1f;
            else
                Time.timeScale = 1.5f;
            gameBoost = !gameBoost;
            gameSlowed = false;
            gamePaused = false;
        }

    }
    public void EndGame()
    {
        PlayerPrefs.SetInt("Kills", kills);
        PlayerPrefs.SetInt("Total", totalBullets);
        PlayerPrefs.SetInt("hits", hitBullets);
        PlayerPrefs.SetInt("Time", ((int)Time.timeSinceLevelLoad));
        PlayerPrefs.SetInt("show_hs", 0);
        SceneManager.LoadScene("ScoreScene");

    }
}


