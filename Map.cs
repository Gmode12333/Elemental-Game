using System;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;

[Serializable]
public class Map : MonoBehaviour
{
    [Header("Map General")]
    public string Name;
    public int DifficultyLevel;
    public int totalMob;
    public int totalBoss;
    public int totalExpDrop;
    public int totalGoldDrop;
    [Header("Map Difficulty")]
    public int totalCompleteTime;
    public GameObject[] normalEnemy;
    public GameObject[] Boss;
    [Header("UI")]
    [SerializeField] TextMeshProUGUI nameUI;
    [SerializeField] TextMeshProUGUI levelUI;
    [SerializeField] TextMeshProUGUI mobUI;
    [SerializeField] TextMeshProUGUI bossUI;
    [SerializeField] TextMeshProUGUI expUI;
    [SerializeField] TextMeshProUGUI goldUI;
    private void Start()
    {
        SetUI();
    }
    private void SetUI()
    {
        nameUI.text = Name;
        levelUI.text = DifficultyLevel.ToString();
        mobUI.text = totalMob.ToString();
        bossUI.text = totalBoss.ToString();
        expUI.text = totalExpDrop.ToString();
        goldUI.text = totalGoldDrop.ToString();
    }
    public void IncreaseLevel()
    {
        DifficultyLevel++;
        totalMob++;
        BossOrNot();
        totalExpDrop += 3/4;
        totalGoldDrop += 3/4;
    }
    private void BossOrNot()
    {
        if(DifficultyLevel % 5 == 0)
        {
            totalBoss = 1;
        }
        else
        {
            totalBoss = 0;
        }
    }
}
