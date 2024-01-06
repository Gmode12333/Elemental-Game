using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEditor;
using System;
using Unity.Services.CloudSave.Models;

public class CloudSaveScript : MonoBehaviour
{
    private ISet<string> keys = new HashSet<string>
    {
        "Level",
        "Gem",
        "SP",
        "CT",
        "WT"
    };


    private async void Awake()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        LoadPlayerData();
    }
    public void NewPlayerData()
    {
        GameManager.Instance.userLevel = 1;
        GameManager.Instance.userGem = 0;
        GameManager.Instance.userSP = 0;
        GameManager.Instance.userWT = 1;
        GameManager.Instance.userCT = 1;
    }
    public async void SavePlayerData()
    {
        var userdata = new Dictionary<string, object> 
        { 
            { "Level", GameManager.Instance.userLevel },
            { "Gem", GameManager.Instance.userGem },
            { "SP", GameManager.Instance.userSP },
            { "CT", GameManager.Instance.userCT },
            { "WT", GameManager.Instance.userWT }
        };
        await CloudSaveService.Instance.Data.Player.SaveAsync(userdata);
    }

    private async void LoadPlayerData()
    {
        Dictionary<string, Item> userdata = await CloudSaveService.Instance.Data.Player.LoadAsync(keys);
        GameManager.Instance.userLevel = userdata["Level"].Value.GetAs<int>();
        GameManager.Instance.userGem = userdata["Gem"].Value.GetAs<int>();
        GameManager.Instance.userSP = userdata["SP"].Value.GetAs<int>();
        GameManager.Instance.userCT = userdata["CT"].Value.GetAs<int>();
        GameManager.Instance.userWT = userdata["WT"].Value.GetAs<int>();
    }
}
