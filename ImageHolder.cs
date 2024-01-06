using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageHolder : GlobalReference<ImageHolder>
{
    public List<EMImage> EMIcon = new List<EMImage>();
    public Image GetSelectImage()
    {
        int emindex =  PlayerPrefs.GetInt("selectedElement");
        int iconIndex = PlayerPrefs.GetInt("IconIndex");
        return EMIcon[emindex].Elements[iconIndex - 1];
    }
}
[Serializable]
public class EMImage
{
    public SkillElement element;
    public List<Image> Elements;
}
