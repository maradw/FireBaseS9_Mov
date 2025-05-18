using UnityEngine;

public class PlayerFeesh : MonoBehaviour
{
    public float fallLimitY = -10f;
    private GameManager gameManager;
    private FirebaseManager firebaseManager;

    void Start()
    {
        
    }

    void Update()
    {
        if (transform.position.y < fallLimitY)
        {
            gameManager.EndGame();
            SaveScoreToFirebase();
        }
    }

    void SaveScoreToFirebase()
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "SinNombre");
        int score = gameManager.GetScore();

        firebaseManager.SaveScore(playerName, score); 
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        if (score > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", score);
            Debug.Log("¡Nuevo récord personal!");
        }
    }


}
