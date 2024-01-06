using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadPlayerProfile : MonoBehaviour
{
    public Transform playerData;
    public GameObject NewGameObject; 
    public void ChooseData()
    {
        if(playerData.childCount > 0)
        {
            Debug.Log("Select User: " + PlayerPrefs.GetString("PlayerData_Username"));
            NewGameObject.SetActive(false);
        }
        else
        {
            Debug.Log("No User To Choose");
        }
    }
}
