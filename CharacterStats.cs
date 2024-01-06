using Newtonsoft.Json;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    [Header("Character Scriptable Object")]
    public CharacterProfile[] characterProfiles;
    public CharacterSelection selection;
    public bool canUnlock;
    public bool isUnlock;
    private int selectedCharacter = 0;
    [Header("Character Info")]
    public int characterATK;
    public int characterDEF;
    public int characterHP;
    public int characterEM;
    public GameObject LockUI;
    [SerializeField] TextMeshProUGUI ticketNeed;
    [Header("Character UI")]
    [SerializeField] TextMeshProUGUI Name;
    [SerializeField] TextMeshProUGUI Level;
    [SerializeField] TextMeshProUGUI ATK;
    [SerializeField] TextMeshProUGUI DEF;
    [SerializeField] TextMeshProUGUI HP;
    [SerializeField] TextMeshProUGUI EM;
    [SerializeField] GameObject PlayButton;
    [Header("Level Gold")]
    [SerializeField] int totalGold;
    [SerializeField] int goldTimeLevel;
    [SerializeField] int currentLevel;
    [SerializeField] TextMeshProUGUI goldText;
    [Header("Additional Weapon Points")]
    [SerializeField] TextMeshProUGUI weaponATK;
    [SerializeField] TextMeshProUGUI weaponDEF;
    [SerializeField] TextMeshProUGUI weaponHP;
    [SerializeField] TextMeshProUGUI weaponEM;
    private void Awake()
    {
        SetCharacterInfo();
        //ResetCharacterLevel();
    }
    private void Start()
    {
        selection = GetComponent<CharacterSelection>();
        PrivateSet();
        SetUI();
    }
    private void PrivateSet()
    {
        characterATK = PlayerPrefs.GetInt(characterProfiles[selectedCharacter].Name + "Attack");
        characterDEF = PlayerPrefs.GetInt(characterProfiles[selectedCharacter].Name + "Defend");
        characterHP = PlayerPrefs.GetInt(characterProfiles[selectedCharacter].Name + "Health");
        characterEM = PlayerPrefs.GetInt(characterProfiles[selectedCharacter].Name + "EM");
    }
    private void CheckGold()
    {
        totalGold = goldTimeLevel * currentLevel;
    }
    private void SetUI()
    {
        Name.text = characterProfiles[selectedCharacter].Name;
        Level.text = PlayerPrefs.GetInt(characterProfiles[selectedCharacter].Name).ToString();
        ATK.text = characterATK.ToString();
        DEF.text = characterDEF.ToString();
        HP.text = characterHP.ToString();
        EM.text = characterEM.ToString();

        weaponATK.text = " + " + PlayerPrefs.GetInt("currentWeaponATK").ToString();
        weaponDEF.text = " + " + PlayerPrefs.GetInt("currentWeaponDEF").ToString();
        weaponHP.text = " + " + PlayerPrefs.GetInt("currentWeaponHP").ToString();
        weaponEM.text = " + " + PlayerPrefs.GetInt("currentWeaponEM").ToString();

        ticketNeed.text = characterProfiles[selectedCharacter].TicketRequire.ToString();
        string unlock = PlayerPrefs.GetString("Unlock" + characterProfiles[selectedCharacter].Name);
        if (unlock == "IsUnlock")
        {
            CharacterLevelUpUI();
            LockUI.gameObject.SetActive(false);
            isUnlock = true;
        }
        else
        {
            isUnlock = false;
            LockUI.gameObject.SetActive(true);
            if (GameManager.Instance.userCT >= characterProfiles[selectedCharacter].TicketRequire)
            {
                ticketNeed.color = Color.green;
                canUnlock = true;
            }
            else
            {
                ticketNeed.color = Color.red;
                canUnlock = false;
            }
        }
    }
    public void NextCharacterInfo()
    {
        selectedCharacter = (selectedCharacter + 1) % characterProfiles.Length;
        PrivateSet();
    }
    public void PreviousCharacterInfo()
    {
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter += characterProfiles.Length;
        }
        PrivateSet();
    }
    public void SetUnlockCharacter()
    {
        if (!canUnlock)
            return;
        GameManager.Instance.SpendCharacterTicket(characterProfiles[selectedCharacter].TicketRequire);
        PlayerPrefs.SetString("Unlock" +characterProfiles[selectedCharacter].Name, "IsUnlock");
        SoundManager.Instance.PlayerCharacterUnlock(characterProfiles[selectedCharacter].Name + "Voice");
    }
    private void Update()
    {
        if (isUnlock)
        {
            selection.characters[selectedCharacter].GetComponent<Image>().color = Color.white;
            PlayButton.SetActive(true);
        }
        else
        {
            selection.characters[selectedCharacter].GetComponent<Image>().color = Color.black;
            PlayButton.SetActive(false);

        }
        SetUI();
        SetCharacterInfo();
        CheckGold();
    }
    public void SetCharacterInfo()
    {
        GameManager.Instance.Info = characterProfiles[selectedCharacter];
        GameManager.Instance.SetInfo();
        GameManager.Instance.SetStatsInGame();
    }
    public void CharacterLevelUpUI()
    {
        currentLevel = PlayerPrefs.GetInt(characterProfiles[selectedCharacter].Name);
        goldText.text = totalGold.ToString();

        if (totalGold > GameManager.Instance.userGem)
        {
            goldText.color = Color.red;
        }
        else
        {
            goldText.color = Color.green;
        }
    }
    public void NextGoldLevel()
    {
        if (totalGold > GameManager.Instance.userGem)
            return;
        GameManager.Instance.SpendGold(totalGold);
        currentLevel++;
        GameManager.Instance.userSP++;
        PlayerPrefs.SetInt(characterProfiles[selectedCharacter].Name, currentLevel);
        totalGold = currentLevel * goldTimeLevel;
        StatsUP();
    }
    public void StatsUP()
    {
        characterATK += 25;
        characterDEF += 25;
        characterHP += 25;
        characterEM += 25;
        characterProfiles[selectedCharacter].SaveInfo(characterATK, characterDEF, characterHP, characterEM);
        Debug.Log("Save Character Data");
        SaveData();
    }
    public void ResetCharacterLevel()
    {
        PlayerPrefs.SetInt(characterProfiles[selectedCharacter].Name, 1);
        PlayerPrefs.SetInt(characterProfiles[selectedCharacter].Name + "Attack", characterProfiles[selectedCharacter].Attack);
        PlayerPrefs.SetInt(characterProfiles[selectedCharacter].Name + "Defend", characterProfiles[selectedCharacter].Defend);
        PlayerPrefs.SetInt(characterProfiles[selectedCharacter].Name + "Health", characterProfiles[selectedCharacter].MaxHealth);
        PlayerPrefs.SetInt(characterProfiles[selectedCharacter].Name + "EM", characterProfiles[selectedCharacter].ElementsMastery);
    }

    public void ResetCurrentUnlockCharacter()
    {
        ResetCharacterLevel();
        PlayerPrefs.SetString("Unlock" + characterProfiles[selectedCharacter].Name, "NotUnlock");
    }

    public void SaveData()
    {
        GameManager.Instance.SaveGameData();
    }
}
