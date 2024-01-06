using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CutScene : MonoBehaviour
{
    public List<string> StoryTimeline;
    public TextMeshProUGUI storyText;
    public Tutorial tutorial;
    private Animator anim;
    [SerializeField] int storyIndex = 0;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void NextStoryLine()
    {
        storyText.text = StoryTimeline[storyIndex];
        storyIndex++;
    }
    public void TurnOffTheCutScene()
    {
        GameManager.Instance.newUser = false;
    }
    public void TurnOnTheTutorial()
    {
        if(tutorial != null)
        {
            tutorial.gameObject.SetActive(true);
        }
    }
    public void Turnoff()
    {
        this.gameObject.SetActive(false);
    }
    public void SaveCurrentData()
    {
        GameManager.Instance.SaveGameData();
    }
}
