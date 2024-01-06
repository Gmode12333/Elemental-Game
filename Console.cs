using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    public GameObject CheatConsolePanel;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.BackQuote))
        {
            CheatConsolePanel.SetActive(!CheatConsolePanel.activeSelf);
        }
    }
    public void CheatGEM()
    {
        GameManager.Instance.userGem += 1000;
    }
    public void CheatSP()
    {
        GameManager.Instance.userSP += 10;
    }
    public void CheatCT()
    {
        GameManager.Instance.userCT += 10;
    }
    public void CheatWT()
    {
        GameManager.Instance.userWT += 10;
    }
}
