using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CalculateStats
{
    public static int characterHP;
    public static int characterATK;
    public static int characterDEF;
    public static int characterEM;

    public static Dictionary<string, int> SkillPoints = new Dictionary<string, int>()
    {
        {"SkillATK", PlayerPrefs.GetInt("SkillATK")},
        {"SkillDEF", PlayerPrefs.GetInt("SkillDEF")},
        {"SkillHP", PlayerPrefs.GetInt("SkillHP")},
        {"SkillEM", PlayerPrefs.GetInt("SkillEM")},
        {"SkillASPD", PlayerPrefs.GetInt("SkillASPD")},
        {"SkillCD", PlayerPrefs.GetInt("SkillCD")},
        {"SkillCR", PlayerPrefs.GetInt("SkillCR")},
        {"SkillRegen", PlayerPrefs.GetInt("SkillRegen")},
        {"SkillMSPD", PlayerPrefs.GetInt("SkillMSPD")}
    };
    public static void UpdateSkillPoints(string key, int value)
    {
        if (SkillPoints.ContainsKey(key))
        {
            SkillPoints[key] += value;
        }
    }
    public static void ResetSkillPoints(string key)
    {
        if (SkillPoints.ContainsKey(key))
        {
            SkillPoints[key] = 0;
        }
    }
    public static int GetKeyValue(string key)
    {
        return SkillPoints[key];
    }
    public static void SetStats(int ATK, int DEF, int HP, int EM)
    {
        characterHP = HP;
        characterATK = ATK;
        characterDEF = DEF;
        characterEM = EM;
    }
    public static void SaveStats()
    {
        PlayerPrefs.SetInt("SkillATK", SkillPoints["SkillATK"]);
        PlayerPrefs.SetInt("SkillDEF", SkillPoints["SkillDEF"]);
        PlayerPrefs.SetInt("SkillHP", SkillPoints["SkillHP"]);
        PlayerPrefs.SetInt("SkillEM", SkillPoints["SkillEM"]);
        PlayerPrefs.SetInt("SkillASPD", SkillPoints["SkillASPD"]);
        PlayerPrefs.SetInt("SkillCD", SkillPoints["SkillCD"]);
        PlayerPrefs.SetInt("SkillCR", SkillPoints["SkillCR"]);
        PlayerPrefs.SetInt("SkillRegen", SkillPoints["SkillRegen"]);
        PlayerPrefs.SetInt("SkillMSPD", SkillPoints["SkillMSPD"]);
    }
    public static void ResetAllStats()
    {
        PlayerPrefs.SetInt("SkillATK", 0);
        PlayerPrefs.SetInt("SkillDEF", 0);
        PlayerPrefs.SetInt("SkillHP", 0);
        PlayerPrefs.SetInt("SkillEM", 0);
        PlayerPrefs.SetInt("SkillASPD", 0);
        PlayerPrefs.SetInt("SkillCD", 0);
        PlayerPrefs.SetInt("SkillCR", 0);
        PlayerPrefs.SetInt("SkillRegen", 0);
        PlayerPrefs.SetInt("SkillMSPD", 0);
    }
}