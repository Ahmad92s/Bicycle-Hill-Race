using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject winPanel, losePanel, HUD, menuPanel;

    Touch touch;

    internal bool headHit, gameOver;

    private void Awake()
    {
        instance = this;

        PauseTime();
    }
    private void Start()
    {
        HeadCollider.hitHead += handleLoss;
        HeadCollider.hitHead += handleLoseScreenFlag;
        PlayerCollision.finish += handleFinishLane;
    }
    private void Update()
    {
        if (!gameOver)
        {
            if (Input.touchCount > 0 || Input.GetKey(KeyCode.Space))
            {
                if (touch.phase == TouchPhase.Began)
                {
                    hideMenu();
                }
            }
        }
    }


    private void OnDisable()
    {
        HeadCollider.hitHead -= handleLoss;
        PlayerCollision.finish -= handleFinishLane;
        HeadCollider.hitHead -= handleLoseScreenFlag;
    }

    void handleLoss()
    {
        Debug.Log("loss");

        AudioManager.instance.PlaySound("lose");

        gameOver = true;
        Invoke("showLose", 0.6f);

    }
    void handleWin()
    {
        Debug.Log("win");
        gameOver = true;

        AudioManager.instance.PlaySound("win");

        Invoke("showWin", 0.6f);

        Invoke("SlowTimeDown", 0.5f);
    }

    void SlowTimeDown()
    {
        Time.timeScale = 0.4f;
        Invoke("PauseTime", 1.3f);
    }

    void PauseTime()
    {
        Time.timeScale = 0f;
    }

    void handleFinishLane(bool win)
    {
        if (win)
        {
            handleWin();
        }
        else
        {
            handleLoss();
        }
    }

    void hideMenu()
    {
        Time.timeScale = 1;

        menuPanel.gameObject.SetActive(false);
        HUD.gameObject.SetActive(true);
        losePanel.gameObject.SetActive(false);
        winPanel.gameObject.SetActive(false);
    }

    void showLose()
    {
        menuPanel.gameObject.SetActive(false);
        HUD.gameObject.SetActive(false);
        losePanel.gameObject.SetActive(true);
        winPanel.gameObject.SetActive(false);
    }

    void showWin()
    {
        menuPanel.gameObject.SetActive(false);
        HUD.gameObject.SetActive(false);
        losePanel.gameObject.SetActive(false);
        winPanel.gameObject.SetActive(true);
    }

    void handleLoseScreenFlag()
    {
        headHit = true;
    }
}
