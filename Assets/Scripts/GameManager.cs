using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnGameStart;
    [SerializeField] private DatabaseHandler databaseHandler;
    [SerializeField] private PlayerFeesh playerFeesh;
    public static event Action OnGamePaused;
    public static event Action OnGameResumed;
    private bool isGameRunning = false;
    private bool isPaused = false;
    [SerializeField] GameData gameData;
    private float score = 0;
    private int finalScore = 0;


    private void Start()
    {
        StartGame();
        print("name" + databaseHandler.GetName());
        databaseHandler.CreateNewPlayer(gameData.name);
    }
    private void OnEnable()
    {
        PlayerFeesh.OnGameOver += EndGame;
    }
    private void OnDisable()
    {
        PlayerFeesh.OnGameOver -= EndGame;
    }
    public void StartGame()
    {
        isGameRunning = true;
        isPaused = false;
        score = 0;
        Time.timeScale = 1f; 
        Debug.Log("Game Started");
        OnGameStart?.Invoke();
    }


    public void EndGame()
    {
        isGameRunning = false;
        Time.timeScale = 1f;
        Debug.Log("Game Over");
        Debug.Log("Game Over. Score: " + playerFeesh.GetScore());
        databaseHandler.SaveScore(playerFeesh.GetScore());
    }

    public void PauseGame()
    {
        if (!isGameRunning || isPaused) return;

        isPaused = true;
        Time.timeScale = 0f;
        Debug.Log("Game Paused");
        OnGamePaused?.Invoke();
    }

    public void ResumeGame()
    {
        if (!isGameRunning || !isPaused) return;

        isPaused = false;
        Time.timeScale = 1f;
        Debug.Log("Game Resumed");
        OnGameResumed?.Invoke();
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    
}
