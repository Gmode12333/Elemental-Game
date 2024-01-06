using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EMHolder : MonoBehaviour
{
    public Image mainImage;

    public Image[] BG;
    private void Awake()
    {
        mainImage.sprite = BG[0].sprite;
    }
    private void Update()
    {
        mainImage.sprite = BG[ElementSelection.selectedElements].sprite;
    }
}
