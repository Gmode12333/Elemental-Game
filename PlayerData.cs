using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UserData")]
[System.Serializable]
public class PlayerData : ScriptableObject
{
    public int userLevel;
    public int userGem;
    public int userSP;
    public int userWT;
    public int userCT;
}
