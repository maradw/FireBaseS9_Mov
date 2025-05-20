using System;
using System.Collections;
using System.Diagnostics;
using Firebase.Database;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DatabaseHandler : MonoBehaviour
{
    private string userID;
    private DatabaseReference reference;
    [SerializeField] private TMP_InputField inputField;
    private User getUser;
    private string playerKey = "";
    //[SerializeField] GameData gameData;
    private void Awake()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
    }

    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
      
    }

    public void LoadInfo()
    {
        Invoke(nameof(GetUserInfo), 1f);
    }
    public void SaveInfo()
    {
        CreateUser();
    }
    
    private void CreateUser()
    {
        string playerName = inputField.text;
        User newUser = new User(playerName, 0);
        string json = JsonUtility.ToJson(newUser);


        reference.Child("users").Child(userID).SetRawJsonValueAsync(json);
    }
    public string GetName()
    {
        string name = inputField.text;
        return name;
    }
    public void SaveName()
    {
        string playerName = inputField.text;
       // gameData.name = playerName;
    }
    public void CreateNewPlayer()
    {

        string playerName = inputField.text;
        User newPlayer = new User(playerName, 0);
        string json = JsonUtility.ToJson(newPlayer);


        DatabaseReference newPlayerRef = reference.Child("users").Child(userID).Child("players").Push();
        playerKey = newPlayerRef.Key; 

        newPlayerRef.SetRawJsonValueAsync(json);
    }
    public void SaveScore(int score)
    {
        if (string.IsNullOrEmpty(playerKey))
        {
            UnityEngine.Debug.LogWarning("No playerKey definida. Llama a CreateNewPlayer() primero.");
            return;
        }

        reference.Child("users").Child(userID).Child("players").Child(playerKey).Child("scoreHeight").SetValueAsync(score);
        reference.Child("users").Child(userID).Child("players").Child(playerKey).Child("scores").Push().SetValueAsync(score);
    }
    public void SaveScore2(int score)
    {
        print("score sended");
        reference.Child("users").Child(userID).Child("scoreHeight").SetValueAsync(score);
        print("success");
    }

    private IEnumerator GetFirstName(Action<string> onCallBack)
    {
        var userNameData = reference.Child("users").Child(userID).Child("firstName").GetValueAsync();


        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);


        if (userNameData != null)
        {
            DataSnapshot snapshot = userNameData.Result;
            onCallBack?.Invoke(snapshot.Value.ToString());
        }
    }


    private IEnumerator GetLastName(Action<string> onCallBack)
    {
        var userNameData = reference.Child("users").Child(userID).Child("lastName").GetValueAsync();


        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);


        if (userNameData != null)
        {
            DataSnapshot snapshot = userNameData.Result;
            onCallBack?.Invoke(snapshot.Value.ToString());
        }
    }


    public void GetUserInfo()
    {
        StartCoroutine(GetFirstName(PrintData));
        StartCoroutine(GetLastName(PrintData));
    }


    private void PrintData(string name)
    {
        print(name);
    }


    private void PrintData(int code)
    {
       print(code);
    }
    public class User
    {
        public string nickName;
        public int scoreHeight;


        public User(string nickname, int score)
        {

            this.nickName = nickname;
            this.scoreHeight = score;
        }
    }
}
