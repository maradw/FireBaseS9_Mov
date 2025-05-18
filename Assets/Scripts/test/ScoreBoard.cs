using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] scoreEntries;
    [SerializeField] private FirebaseManager firebaseManager;

    void Start()
    {
        firebaseManager.GetTop5Scores(UpdateUI);
    }
    void UpdateUI(List<FirebaseManager.ScoreData> scores)
    {
        for (int i = 0; i < scoreEntries.Length; i++)
        {
            if (i < scores.Count)
            {
                scoreEntries[i].text = $"{scores[i].playerName} - {scores[i].score}";
            }
            else
            {
                scoreEntries[i].text = "---";
            }
        }
    }
}
