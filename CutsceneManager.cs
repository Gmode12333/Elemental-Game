using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public GameObject Cutscene;
    private void LateUpdate()
    {
        if(GameManager.Instance.newUser)
        {
            Cutscene.SetActive(true);
        }
        else
        {
            Cutscene.SetActive(false);
        }
    }
}
