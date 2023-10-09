using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{

    public Text difficultyText;
    public float crosswordTime;

    public float timeRemaining;
    public bool timerIsRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        crosswordTime = 75.0f;
        timeRemaining = crosswordTime;
        timerIsRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)

        {

            if (timeRemaining > 0)

            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {

                timerIsRunning = false;
            }

        }
    }

    public void WhichDifficulty()
    {
        if (difficultyText.text == "KOLAY")
        {
            difficultyText.text = "ORTA";
        }
        else if (difficultyText.text == "ORTA")
        {
            difficultyText.text = "ZOR";
        }
        else if (difficultyText.text == "ZOR")
        {
            difficultyText.text = "KOLAY";
        }
        
    }

    public void GameOn()
    {
        

        if (difficultyText.text == "KOLAY")
            crosswordTime = 75.0f;
        else if (difficultyText.text == "ORTA")
            crosswordTime = 60.0f;
        else if (difficultyText.text == "ZOR")
            crosswordTime = 30.0f;

        SceneManager.LoadScene("GameKelime");
        DontDestroyOnLoad(gameObject);
    }

    public void GameOff() 
    {
        SceneManager.LoadScene("HowtoPlay");
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("SampleScene");
        timeRemaining = crosswordTime;
    }

    public void GameOver()
    {
        Application.Quit();
    }
}
