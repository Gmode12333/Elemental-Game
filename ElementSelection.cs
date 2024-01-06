using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ElementSelection : MonoBehaviour
{
    [Header("EM Stats && UI")]
    public static int selectedElements = 0;
    [SerializeField] Image[] elementsImage;
    [SerializeField] Image[] BG;
    [SerializeField] GameObject[] skillTree;
    public List<Skill> passiveSkills;
    [Header("UI && Text")]
    [SerializeField] private TextMeshProUGUI Name;
    [SerializeField] private TextMeshProUGUI ATK;
    [SerializeField] private TextMeshProUGUI DEF;
    [SerializeField] private TextMeshProUGUI HP;
    [SerializeField] private TextMeshProUGUI EM;
    [SerializeField] private TextMeshProUGUI Regen;
    [SerializeField] private TextMeshProUGUI ASPD;
    [SerializeField] private TextMeshProUGUI CR;
    [SerializeField] private TextMeshProUGUI CD;
    [SerializeField] private TextMeshProUGUI MSPD;
    private void Awake()
    {
        selectedElements = 0;
        LoadSkill();
    }

    public static List<string> skillTag = new List<string>();
    public static List<string> passiveSkill = new List<string>();
    public void NextElements()
    {
        BG[selectedElements].gameObject.SetActive(false);
        elementsImage[selectedElements].gameObject.SetActive(false);
        skillTree[selectedElements].gameObject.SetActive(false);
        selectedElements = (selectedElements + 1) % elementsImage.Length;
        skillTree[selectedElements].gameObject.SetActive(true);
        elementsImage[selectedElements].gameObject.SetActive(true);
        BG[selectedElements].gameObject.SetActive(true);
    }
    public void PreviousElements()
    {
        BG[selectedElements].gameObject.SetActive(false);
        elementsImage[selectedElements].gameObject.SetActive(false);
        skillTree[selectedElements].gameObject.SetActive(false);
        selectedElements--;
        if (selectedElements < 0)
        {
            selectedElements += elementsImage.Length;
        }
        skillTree[selectedElements].gameObject.SetActive(true);
        elementsImage[selectedElements].gameObject.SetActive(true);
        BG[selectedElements].gameObject.SetActive(true);
    }
    public void GotoSelectCharacter()
    {
        PlayerPrefs.SetInt("selectedElement", selectedElements);
        SceneManager.LoadScene("SelectCharacterScene");
    }
    public void ReturnHome()
    {
        SceneManager.LoadScene("HomeScene");
    }
    private void SetUI()
    {
        Name.text = BG[selectedElements].gameObject.name;
        ATK.text = " + " + CalculateStats.GetKeyValue("SkillATK").ToString();
        DEF.text = " + " + CalculateStats.GetKeyValue("SkillDEF").ToString();
        HP.text = " + " + CalculateStats.GetKeyValue("SkillHP").ToString();
        EM.text = " + " + CalculateStats.GetKeyValue("SkillEM").ToString();
        Regen.text = " + " + CalculateStats.GetKeyValue("SkillRegen").ToString() + "%";
        ASPD.text = " + " + CalculateStats.GetKeyValue("SkillASPD").ToString() + "%";
        CR.text = " + " + CalculateStats.GetKeyValue("SkillCR").ToString() + "%";
        CD.text = " + " + CalculateStats.GetKeyValue("SkillCD").ToString() + "%";
        MSPD.text = " + " + CalculateStats.GetKeyValue("SkillMSPD").ToString() + "%";
    }
    private void Update()
    {
        SetUI();
    }
    public static void SaveSkill()
    {
        for (int i = 0; i < skillTag.Count; i++)
        {
            PlayerPrefs.SetString("SaveSkill" + i, skillTag[i]);
        }
        PlayerPrefs.SetInt("listSkill", skillTag.Count);
        PlayerPrefs.Save();
    }
    public static void LoadSkill()
    {
        skillTag.Clear();
        int count = PlayerPrefs.GetInt("listSkill");
        for (int i = 0; i < count; i++)
        {
            skillTag.Add(PlayerPrefs.GetString("SaveSkill" + i));
        }
    }
}
