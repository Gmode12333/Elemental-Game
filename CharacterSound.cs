using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSound : MonoBehaviour
{
    public void PlayClickSound()
    {
        SoundManager.Instance.PlaySFX("Click");
    }
    public void UnlockCharacterSound()
    {
        SoundManager.Instance.PlayerCharacterUnlock("Unlock");
    }
}
