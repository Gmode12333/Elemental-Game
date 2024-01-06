using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelScene : MonoBehaviour
{
    public TextMeshProUGUI DugeonLevel;
    public TextMeshProUGUI TotalGem;
    public TextMeshProUGUI TotalExp;

    private void Start()
    {
        //ResetDugeon();
        //PlayerPrefs.SetInt("currentLevel", 9);
        int currentLevel = PlayerPrefs.GetInt("currentLevel") + 1;
        DugeonLevel.text =  currentLevel.ToString();
        TotalGem.text = ((currentLevel * 25) * 2).ToString();
        TotalExp.text = (currentLevel * 100).ToString();
    }
    public void ReturnHome()
    {
        SceneManager.LoadScene("SelectCharacterScene");
    }
    public void GoToDugeon()
    {
        SceneManager.LoadScene("GameplayScene");
    }
    public void Tutorial()
    {
        PlayerPrefs.SetInt("IsTrain", 0);
    }
    public void ResetDugeon()
    {
        PlayerPrefs.SetInt("currentLevel", 0);
    }
}
