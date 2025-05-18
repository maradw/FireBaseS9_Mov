using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Student", menuName = "ScriptableObjects/Example/Student")]
public class Players : ScriptableObject
{
    [SerializeField] private Player playerData;
    [SerializeField] private List< Player> players;
    public Player PlayerData => playerData;


    public Player GetBasicStudentData()
    {
        return new Player( "Nickname", 18);
    }
}
