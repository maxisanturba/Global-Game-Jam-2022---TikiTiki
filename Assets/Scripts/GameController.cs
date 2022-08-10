using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameManager;
using System;

public class GameController : MonoBehaviour
{
    public bool gameOver;
    public Text timeLeftText;
    public Text timeLeftText2;
    public Text bluePointsText;
    public Text bluePointsText2;
    public Text redPointsText;
    public Text redPointsText2;
    public Text playerWinner;
    public float bluePlayerPoints;
    public float redPlayerPoints;
    public float totalTime;
    private int totalTimeInt;
    public Button reMatchBut;
    public Button mainMenuBut;    
    public Button reMatchPauseBut;
    public Button mainMenuPauseBut;

    public GameObject gameOverPanel;
    public GameObject pausePanel;

    public AudioSource audioSource;
    public AudioClip ambientClip;

    private void Start()
    {
        audioSource.PlayOneShot(ambientClip);

        reMatchBut.onClick.AddListener(() => GameSettings.ChangeScene(1));
        mainMenuBut.onClick.AddListener(() => GameSettings.ChangeScene(0));
        
        reMatchPauseBut.onClick.AddListener(delegate
        {
            GameSettings.ChangeScene(1);
            GameSettings.gameIsPaused = false;
            GameSettings.PauseGame();
        });
        mainMenuPauseBut.onClick.AddListener(delegate
        {
            GameSettings.ChangeScene(0);
            GameSettings.gameIsPaused = false;
            GameSettings.PauseGame();
        });

        gameOver = false;
        totalTimeInt = (int)totalTime;
        StartCoroutine(TimeToWin());
    }
    private void Update()
    {
        PauseControl();
    }
    private void FixedUpdate()
    {
        CountingTime();
        bluePointsText.text = bluePlayerPoints.ToString();
        redPointsText.text = redPlayerPoints.ToString(); 
        bluePointsText2.text = bluePlayerPoints.ToString();
        redPointsText2.text = redPlayerPoints.ToString();
    }
    public IEnumerator TimeToWin()
    {
        yield return new WaitForSeconds(totalTime);

        gameOver = true;
        gameOverPanel.SetActive(true);
        if(bluePlayerPoints > redPlayerPoints)
        {
            playerWinner.color = Color.blue;
            playerWinner.text = "Ice Win";
        }
        if(bluePlayerPoints < redPlayerPoints)
        {
            playerWinner.color = Color.red;
            playerWinner.text = "Magma Win";
        }
        if (bluePlayerPoints == redPlayerPoints)
        {
            playerWinner.text = "Tie";
        }
    }
    private void CountingTime()
    {
        if(totalTime >= 0)
        {
            totalTime -= 1 * Time.deltaTime;
            totalTimeInt = ((int)totalTime);
            timeLeftText.text = totalTimeInt.ToString();
            timeLeftText2.text = totalTimeInt.ToString();
        }
    }
    private void PauseControl()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            pausePanel.active = !pausePanel.active;
            GameSettings.gameIsPaused = !GameSettings.gameIsPaused;
            GameSettings.PauseGame();
        }
    }
}
