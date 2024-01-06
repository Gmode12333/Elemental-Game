using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public TextMeshProUGUI Title;
    public TextMeshProUGUI GEM;
    public TextMeshProUGUI EXP;
    public TextMeshProUGUI TimeLeft;
    public Image Background;
    public Color wincolor;
    public Color losecolor;
    public float timeReturnHome;

    private int totalGem;
    private int totalExp;
    public void LoseText()
    {
        Background.color = losecolor;
        Title.text = "You Lose";

        totalExp = InGameManager.Instance.totalExp / 4;
        totalGem = InGameManager.Instance.totalGem / 4;

        GEM.text = totalGem.ToString();
        EXP.text = totalExp.ToString();

        AddReward();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }    
    public void WinText()
    {
        Background.color = wincolor;
        Title.text = "You Win";

        totalGem = InGameManager.Instance.totalGem;
        totalExp = InGameManager.Instance.totalExp;

        GEM.text = totalGem.ToString();
        EXP.text = totalExp.ToString();

        AddReward();
        Cursor.visible = true;
    }
    public void CompleteMission()
    {
        Background.color = wincolor;
        Title.text = "Champion You Have Won!";

        totalGem = InGameManager.Instance.totalGem;
        totalExp = InGameManager.Instance.totalExp;

        GEM.text = totalGem.ToString();
        EXP.text = totalExp.ToString();

        Cursor.visible = true;
        AddReward();
        PlayerPrefs.SetString("Mission", "Complete");
    }
    public void AddReward()
    {
        GameManager.Instance.ExperieceModifier(totalExp);
        GameManager.Instance.userGem += totalGem;
    }
    public void StartTimeReturnHome()
    {
        StartCoroutine(ReturnHome());
    }

    public void StartTimeToEndCutScene()
    {
        StartCoroutine(BackToBegin());
    }
    IEnumerator ReturnHome()
    {
        while(timeReturnHome > 0)
        {
            yield return new WaitForSeconds(0.1f);
            timeReturnHome -= 0.1f;
            TimeLeft.text = timeReturnHome.ToString("F1") + "S";
            if(timeReturnHome <= 0)
            {
                SceneManager.LoadScene("SelectCharacterScene");
            }
        }
    }
    IEnumerator BackToBegin()
    {
        while (timeReturnHome > 0)
        {
            yield return new WaitForSeconds(0.1f);
            timeReturnHome -= 0.1f;
            TimeLeft.text = timeReturnHome.ToString("F1") + "S";
            if (timeReturnHome <= 0)
            {
                SceneManager.LoadScene("HomeScene");
            }
        }
    }
}
