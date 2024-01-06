using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characters;
    public int selectedCharacter = 0;
    public void NextCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characters.Length;
        characters[selectedCharacter].SetActive(true);
    }
    public void PreviousCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if(selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }
        characters[selectedCharacter].SetActive(true);
    }
    public void GoToDugeon()
    {
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        SceneManager.LoadScene("LevelScene");
    }
    public void ReturnHome()
    {
        SceneManager.LoadScene("HomeScene");
    }
    public void GotoWeapon()
    {
        SceneManager.LoadScene("SelectWeaponScene");
    }
    public void GotoElements()
    {
        SceneManager.LoadScene("SelectElementScene");
    }
    public void OnClicking()
    {
        SoundManager.Instance.PlayerCharacterUnlock(characters[selectedCharacter].name + "Voice");
    }
}
