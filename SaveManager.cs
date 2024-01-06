using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SaveManager : MonoBehaviour
{
    private const string savePath = "/gameData.save";

    public void SaveUserData(int level, int gem, int SP, int CT, int WT, int EXP)
    {
        var data = new GameData { userLevel = level, userGem = gem, userSP = SP, userCT = CT, userWT = WT, userEXP = EXP};

        using (var fileStream = new FileStream(Application.persistentDataPath + savePath, FileMode.Create))
        {
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fileStream, data);
        }
    }
    public bool LoadUserData(out int level, out int gem, out int SP, out int CT, out int WT, out int EXP)
    {
        var path = Application.persistentDataPath + savePath;
        Debug.Log(path);

        if (File.Exists(path))
        {
            using (var fileStream = new FileStream(path, FileMode.Open))
            {
                var binaryFormatter = new BinaryFormatter();
                var data = (GameData)binaryFormatter.Deserialize(fileStream);

                level = data.userLevel;
                gem = data.userGem;
                SP = data.userSP;
                CT = data.userCT;
                WT = data.userWT;
                EXP = data.userEXP;

                return true;
            }
        }
        else
        {
            level = 1;
            gem = 0;
            SP = 0; CT = 0; WT = 0;
            EXP = 0;
            return false;
        }
    }
    public void CreateNewUserData(out int level, out int gem, out int SP, out int CT, out int WT, out int EXP)
    {
        level = 1;
        gem = 0;
        SP = 0;
        CT = 1;
        WT = 1;
        EXP = 0;
    }
}
[Serializable]
public class GameData
{
    public int userLevel;
    public int userGem;
    public int userSP;
    public int userCT;
    public int userWT;
    public int userEXP;
}
