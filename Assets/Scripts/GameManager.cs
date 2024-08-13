using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public Ghost[] GhostObject;
    public Pacman PacmanObject;
    public Transform PelletsTransform;
    public int Score { get; private set; }
    public int Lives { get; private set; }
    public float DelayTime = 2f;

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (Lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        SetScore(Score);
        SetLives(3);
        NewRound();
    }

    public void PacmanEaten()
    {
        PacmanObject.gameObject.SetActive(false);
        SetLives(Lives - 1);

        if (Lives > 0)
        {
           Invoke(nameof(ResetStart), DelayTime);
        }
        else
        {
            GameOver();
        }
    }
    
    public void GhostEaten(Ghost ghost)
    {
        SetScore(Score + ghost.ScorePoint);
    }
    
    private void GameOver()
    {
        for (int i = 0; i < GhostObject.Length; i++)
        {
            GhostObject[i].gameObject.SetActive(false); 
        }
        
        PacmanObject.gameObject.SetActive(false);
    }
    private void ResetStart()
    {
        for (int i = 0; i < GhostObject.Length; i++)
        {
            GhostObject[i].gameObject.SetActive(true); 
        }
        
        PacmanObject.gameObject.SetActive(true);
    }
    private void NewRound()
    {
        foreach (Transform pellet in PelletsTransform)
        {
            pellet.gameObject.SetActive(true);
        }
        
        ResetStart();
    }
    private void SetScore(int score)
    {
       Score = score;
    }

    private void SetLives(int lives)
    {
        Lives = lives;
    }
}
