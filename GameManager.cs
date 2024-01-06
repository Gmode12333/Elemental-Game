using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : GlobalReference<GameManager>
{
    [Header("Player Info")]
    private SaveManager userData;
    public int userLevel;
    public int userGem;
    public int userSP;
    public int userWT;
    public int userCT;
    public int userEXP;
    public int nextEXP;
    [Header("Character Info")]
    public int CHP; 
    public int CATK;
    public int CDEF;
    public int CEM;
    [Header("Weapon Info")]
    public int weaponHP;
    public int weaponATK;
    public int weaponDEF;
    public int weaponEM;
    [Header("Skill Info")]
    public int skillATK;
    public int skillDEF;
    public int skillHP;
    public int skillEM;
    public int skillASPD;
    public int skillRegen;
    public int skillCR;
    public int skillCD;
    public int skillMSPD;
    [Header("Character Profile Info")]
    public CharacterProfile Info;
    public string currentSkill;
    public bool BuffSkill;
    public bool newUser;
    private void Start()
    {
        userData = GetComponent<SaveManager>();
        SetBuffSkill();
        if (userData.LoadUserData(out userLevel, out userGem, out userSP, out userCT, out userWT, out userEXP))
        {
            Debug.Log("Load Success User Data");
            Debug.Log("Level: " + userLevel);
            Debug.Log("Gem: " + userGem);
            Debug.Log("SP: " + userSP);
            Debug.Log("CT: " + userCT);
            Debug.Log("WT: " + userWT);
            Debug.Log("EXP: " + userEXP);
            newUser = false;
        }
        else
        {
            Debug.Log("No Data Found!");
            userData.CreateNewUserData(out userLevel, out userGem, out userSP, out userCT, out userWT, out userEXP);
            Debug.Log("Create New Data Success");
            newUser = true;
        }
        nextEXP = userLevel * 200;
    }
    public void SetBuffSkill()
    {
        if(currentSkill == "Empty")
        {
            BuffSkill = false;
            return;
        }
        int temp = PlayerPrefs.GetInt("BuffSkill");
        if(temp == 1)
        {
            BuffSkill = true;
            Debug.Log("Buff Skill");
        }
        else
        {
            BuffSkill = false;
            Debug.Log("Not Buff Skill");
        }
    }
    public void SaveGameData()
    {
        userData.SaveUserData(userLevel, userGem, userSP, userCT, userWT, userEXP);
        Debug.Log("Save!");
    }
    public void SetInfo()
    {
        CATK = PlayerPrefs.GetInt(Info.Name + "Attack");
        CDEF = PlayerPrefs.GetInt(Info.Name + "Defend");
        CHP = PlayerPrefs.GetInt(Info.Name + "Health");
        CEM = PlayerPrefs.GetInt(Info.Name + "EM");

        weaponATK = PlayerPrefs.GetInt("currentWeaponATK");
        weaponDEF = PlayerPrefs.GetInt("currentWeaponDEF");
        weaponHP = PlayerPrefs.GetInt("currentWeaponHP");
        weaponEM = PlayerPrefs.GetInt("currentWeaponEM");

        skillATK = CalculateStats.GetKeyValue("SkillATK");
        skillDEF = CalculateStats.GetKeyValue("SkillDEF");
        skillHP = CalculateStats.GetKeyValue("SkillHP");
        skillEM = CalculateStats.GetKeyValue("SkillEM");
        skillASPD = CalculateStats.GetKeyValue("SkillASPD");
        skillRegen = CalculateStats.GetKeyValue("SkillRegen");
        skillCR = CalculateStats.GetKeyValue("SkillCR");
        skillCD = CalculateStats.GetKeyValue("SkillCD");
        skillMSPD = CalculateStats.GetKeyValue("SkillMSPD");

        currentSkill = PlayerPrefs.GetString("currentSkill");
    }
    public void SpendSkillPoints(int amount)
    {
        if (userSP <= 0)
        {
            return;
        }
        userSP -= amount;
    }
    public void SpendWeaponTicket(int amount)
    {
        if (userWT <= 0)
        {
            return;
        }
        userWT -= amount;
    }
    public void SpendCharacterTicket(int amount)
    {
        if (userCT <= 0 && userCT < amount)
        {
            return;
        }
        userCT  -= amount;
    }
    public void SpendGold(int amount)
    {
        if (amount > userGem)
        {
            Debug.Log("Not Enough Gold!");
        }
        else
        {
            userGem -= amount;
        }
    }
    public void SetStatsInGame()
    {
        CalculateStats.SetStats(CATK, CDEF, CHP, CEM);
        CATK += PlayerPrefs.GetInt("TotalATK");
        CDEF += PlayerPrefs.GetInt("TotalDEF");
        CHP += PlayerPrefs.GetInt("TotalHP");
        CEM += PlayerPrefs.GetInt("TotalEM");
    }
    public void SaveSkill()
    {
        if(currentSkill == null)
        {
            return;
        }
        PlayerPrefs.SetString("currentSkill", currentSkill);
    }
    public void ExperieceModifier(int amount)
    {
        userEXP += amount;
        if(userEXP >= nextEXP)
        {
            userEXP -= nextEXP;
            userLevel++;
            userCT++;
            userWT++;
            userSP += 2;
            userGem += userLevel * 200;
        }
    }
}

