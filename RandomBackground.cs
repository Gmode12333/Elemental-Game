using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomBackground : MonoBehaviour
{
    public Image MainImage;
    public Image[] Images;
    private void OnEnable()
    {
        var index = Random.Range(0, Images.Length);
        MainImage.sprite = Images[index].sprite;
    }
}
