using Firebase.Database;
using Firebase;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using System;

public class FirebaseManager : MonoBehaviour
{
    private DatabaseReference dbReference;

    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        });
    }

    public void SaveScore(string playerName, int score)
    {
        string key = dbReference.Child("scores").Push().Key;

        ScoreData data = new ScoreData(playerName, score);
        string json = JsonUtility.ToJson(data);

        dbReference.Child("scores").Child(key).SetRawJsonValueAsync(json);
    }

    public void GetTop5Scores(Action<List<ScoreData>> callback)
    {
        dbReference.Child("scores")
            .OrderByChild("score")
            .LimitToLast(5)
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    List<ScoreData> topScores = new List<ScoreData>();
                    foreach (DataSnapshot snapshot in task.Result.Children)
                    {
                        var json = snapshot.GetRawJsonValue();
                        ScoreData score = JsonUtility.FromJson<ScoreData>(json);
                        topScores.Add(score);
                    }

                    topScores.Sort((a, b) => b.score.CompareTo(a.score)); // Descendente
                    callback?.Invoke(topScores);
                }
            });
    }

    [System.Serializable]
    public class ScoreData
    {
        public string playerName;
        public int score;

        public ScoreData(string name, int s)
        {
            playerName = name;
            score = s;
        }
    }
}
