using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActiveDelay : MonoBehaviour
{
    public GameObject UI;

    private void Start()
    {
        UI.SetActive(false);
        StartCoroutine(DelayTime());
    }

    IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(1f);
        UI.SetActive(true);
    }
}
