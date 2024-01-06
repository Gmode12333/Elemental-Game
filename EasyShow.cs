using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EasyShow : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;
    public bool inOffSet;
    public float LerpSpeed;
    public void GotoOffSetLerp()
    {
        if (inOffSet)
        {
            GotoBackDefaultLerp();
            return;
        }
        StartCoroutine(GotoOffSetLerpCoroutine());
    }
    public void GotoBackDefaultLerp()
    {
        StartCoroutine(GotoBackDefaultLerpCoroutine());
    }
    public void UIActiveOn()
    {
        target.SetActive(true);
    }
    public void UIActiveOff()
    {
        target.SetActive(false);
    }
    IEnumerator GotoOffSetLerpCoroutine()
    {
        Vector3 targetPos = target.transform.localPosition + offset;
        while (Vector3.Distance(target.transform.localPosition, targetPos) > 0.1f)
        {
            target.transform.localPosition = Vector3.Lerp(target.transform.localPosition, targetPos, LerpSpeed);
            yield return null;
        }
        target.transform.localPosition = targetPos;
        inOffSet = true;
    }
    IEnumerator GotoBackDefaultLerpCoroutine()
    {
        Vector3 targetPos = target.transform.localPosition - offset;
        while (Vector3.Distance(target.transform.localPosition, targetPos) > 0.1f)
        {
            target.transform.localPosition = Vector3.Lerp(target.transform.localPosition, targetPos, LerpSpeed);
            yield return null;
        }
        target.transform.localPosition = targetPos;
        inOffSet = false;
    }
}
