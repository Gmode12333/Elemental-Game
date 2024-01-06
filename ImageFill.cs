using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFill : MonoBehaviour
{
    private Image image;
    private float fillSpeed = 0.1f;

    private void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(StartFill());
    }
    IEnumerator StartFill()
    {
        if(image.fillAmount == 0)
        {
            while (image.fillAmount != 1)
            {
                yield return new WaitForSeconds(fillSpeed);
                image.fillAmount += 0.1f;
            }
        }
        if(image.fillAmount == 1)
        {
            while(image.fillAmount != 0)
            {
                yield return new WaitForSeconds(fillSpeed);
                image.fillAmount -= 0.1f;
            }
        }
        StartCoroutine(StartFill());
    }
}
