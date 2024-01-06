using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class HomeManager : MonoBehaviour
{
    public void CharactersSelection()
    {
        SceneManager.LoadScene("SelectCharacterScene");
    }
    public void ReturnHome()
    {
        SceneManager.LoadScene("HomeScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void PlayClickSound()
    {
        SoundManager.Instance.PlaySFX("Click");
    }
}
