using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;

public class DataBaseManager : MonoBehaviour
{
    [SerializeField] private string UserID;
    [SerializeField] private Players studentSO;

    private DatabaseReference reference;

    // Start is called before the first frame update
    private void Awake()
    {
        UserID = SystemInfo.deviceUniqueIdentifier;
    }

    private void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void UpdloadStudent()
    {
        Player newStudent = studentSO.GetBasicStudentData();

        string json = JsonUtility.ToJson(newStudent);

        reference.Child("Students").Child(UserID).Child(newStudent.nickName).SetRawJsonValueAsync(json);
    }
}

[System.Serializable]
public class Player
{

    public string nickName;
    public int scoreHeight; 


    public Player(string nickname, int score)
    {
 
        this.nickName = nickname;
        this.scoreHeight = score;
    }
}
