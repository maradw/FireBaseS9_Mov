using System;
using System.Collections;
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
    private void Awake()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
    }

    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;


        //Invoke(nameof(GetUserInfo), 1f);
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
        User newUser = new User(inputField.text, 0);
        string json = JsonUtility.ToJson(newUser);


        reference.Child("users").Child(userID).SetRawJsonValueAsync(json);
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


    /*private IEnumerator GetCodeID(Action<int> onCallBack)
    {
        var userNameData = reference.Child("users").Child(userID).Child(nameof(User.codeID)).GetValueAsync();


        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);


        if (userNameData != null)
        {
            DataSnapshot snapshot = userNameData.Result;
            //(int) -> Casting
            //int.Parse -> Parsing
            //https://teamtreehouse.com/community/when-should-i-use-int-and-intparse-whats-the-difference
            onCallBack?.Invoke(int.Parse(snapshot.Value.ToString()));
        }
    }*/


    public void GetUserInfo()
    {
        StartCoroutine(GetFirstName(PrintData));
        StartCoroutine(GetLastName(PrintData));
        //StartCoroutine(GetCodeID(PrintData));
    }


    private void PrintData(string name)
    {
        Debug.Log(name);
    }


    private void PrintData(int code)
    {
        Debug.Log(code);
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
