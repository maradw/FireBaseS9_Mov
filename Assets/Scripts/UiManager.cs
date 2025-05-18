using UnityEngine;
using TMPro;


public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    //public TextMeshProUGUI healthText;
    [SerializeField] private GameObject namePanel;
    [SerializeField] private GameObject scoresPanel;
    private void OnEnable()
    {
        GameManager.OnGameStart += ShowGame;

        PlayerFeesh.OnScoreUpdated += UpdateScore;
    }

    private void OnDisable()
    {
        GameManager.OnGameStart -= ShowGame;
        PlayerFeesh.OnScoreUpdated -= UpdateScore;
    }

    void ShowGame( )
    {
        Debug.Log(" Game Started");
    }

    void ShowGameOver()
    {
        Debug.Log(" Game Over");
    }

    void UpdateScore(int newScore)
    {
        scoreText.text = "Height " + newScore;
       
    }
    public void ShowPanels(GameObject panelToSet)
    {
        panelToSet.SetActive(true);
    }
    public void HidePanels(GameObject panelToSet)
    {
        panelToSet.SetActive(false);
    }
}