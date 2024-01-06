using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    [SerializeField] private string musicName;

    private void OnEnable()
    {
        if (SoundManager.Instance.currentTheme == musicName)
            return;
        SoundManager.Instance.PlayMusic(musicName);
    }
}
