using UnityEngine;
using System;
using UnityEngine.SocialPlatforms.Impl;
public class PlayerFeesh : MonoBehaviour
{
    public float fallLimitY = -10f;

    [SerializeField] private FirebaseManager firebaseManager;
    public static event Action<int> OnScoreUpdated;
    public float score = 0;
    public static event Action OnGameOver;


    private float currentScore = 0;
    private int finalScore = 0;
    void Start()
    {
        /* gameManager = GetComponent<GameManager>();
         firebaseManager = GetComponent<FirebaseManager>();*/

        if (firebaseManager == null)
            Debug.LogError("firebaseManager no está asignado en el Inspector.");
    }

    void Update()
    {
        if (transform.position.y < fallLimitY)
        {

            OnGameOver?.Invoke();
            Debug.Log("semuriooo");
            //SaveScoreToFirebase();
        }
        if (transform.position.y > currentScore)
        {
            currentScore = transform.position.y;
            finalScore = Mathf.FloorToInt(currentScore);
            OnScoreUpdated?.Invoke(finalScore); 
        }
    }
    public int GetScore()
    {
        return finalScore;
    }
    void SaveScoreToFirebase()
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "SinNombre");
        //int score = gameManager.GetScore();

        /*firebaseManager.SaveScore(playerName, score); 
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        if (score > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", score);
            Debug.Log("¡Nuevo récord personal!");
        }*/
    }


}
