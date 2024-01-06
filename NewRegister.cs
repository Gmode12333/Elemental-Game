using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewRegister : MonoBehaviour
{
    public TextMeshProUGUI userInput;
    public void CreateNewUserData()
    {
        PlayerPrefs.SetString("PlayerData_Username", userInput.text);
        PlayerPrefs.SetInt("PlayerData_UserLevel", 1);
        PlayerPrefs.SetInt("PlayerData_UserGold", 0);
        PlayerPrefs.SetInt("PlayerData_UserSkillPoints", 0);
        PlayerPrefs.SetInt("PlayerData_UserWeaponPoints", 1);
        PlayerPrefs.SetInt("PlayerData_UserCharacterPoints", 1);
        PlayerPrefs.SetInt("UserData", 1);
        PlayerPrefs.Save();
        Debug.Log("Data Save " + userInput.text);
    }
}
