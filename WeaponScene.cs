using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponScene : GlobalReference<WeaponScene>
{
    [Header("Weapons GameObject")]
    [SerializeField] GameObject[] swords;
    [SerializeField] GameObject[] claymore;
    [SerializeField] GameObject[] catalyst;
    [Header("Weapons Stats")]
    [SerializeField] WeaponScriptable[] swordStats;
    [SerializeField] WeaponScriptable[] claymoreStats;
    [SerializeField] WeaponScriptable[] catalystStats;
    [Header("Selected Weapon")]
    [SerializeField] private int selectedWeapons = 0;
    [Header("Weapon UI")]
    [SerializeField] TextMeshProUGUI name;
    [SerializeField] TextMeshProUGUI attack;
    [SerializeField] TextMeshProUGUI defend;
    [SerializeField] TextMeshProUGUI health;
    [SerializeField] TextMeshProUGUI elementsMastery;
    [SerializeField] TextMeshProUGUI ticketUI;
    public GameObject selectButton;
    public GameObject unlockButton;
    [Header("Current State")]
    public bool Sword;
    public bool Claymore;
    public bool Catalyst;
    private SaveItemManager saveItem;
    List<int> idList;
    private int ticket;
    public void NextSword()
    {
        swords[selectedWeapons].SetActive(false);
        selectedWeapons = (selectedWeapons + 1) % swords.Length;
        swords[selectedWeapons].SetActive(true);
    }
    public void PrevSword()
    {
        swords[selectedWeapons].SetActive(false);
        selectedWeapons--;
        if (selectedWeapons < 0)
        {
            selectedWeapons += swords.Length;
        }
        swords[selectedWeapons].SetActive(true);
    }
    public void NextClaymore()
    {
        claymore[selectedWeapons].SetActive(false);
        selectedWeapons = (selectedWeapons + 1) % claymore.Length;
        claymore[selectedWeapons].SetActive(true);
    }
    public void PrevClaymore()
    {
        claymore[selectedWeapons].SetActive(false);
        selectedWeapons--;
        if (selectedWeapons < 0)
        {
            selectedWeapons += claymore.Length;
        }
        claymore[selectedWeapons].SetActive(true);
    }
    public void NextCatalyst()
    {
        catalyst[selectedWeapons].SetActive(false);
        selectedWeapons = (selectedWeapons + 1) % catalyst.Length;
        catalyst[selectedWeapons].SetActive(true);
    }
    public void PrevCatalyst()
    {
        catalyst[selectedWeapons].SetActive(false);
        selectedWeapons--;
        if (selectedWeapons < 0)
        {
            selectedWeapons += catalyst.Length;
        }
        catalyst[selectedWeapons].SetActive(true);
    }
    public void SetDefault()
    {
        Sword = true;
        Catalyst = false;
        Claymore = false;
    }
    public void ChangeToClaymore()
    {
        Claymore = true;
        Sword = false;
        Catalyst = false;
    }
    public void ChangeToCatalyst()
    {
        Catalyst = true;
        Claymore = false;
        Sword = false;
    }

    public void GoBackToCharacter()
    {
        PlayerPrefs.SetInt("selectedWeapon", selectedWeapons);
        if (Sword)
        {
            PlayerPrefs.SetString("weaponType", "sword");
            PlayerPrefs.SetInt("currentWeaponATK", swordStats[selectedWeapons].Attack);
            PlayerPrefs.SetInt("currentWeaponDEF", swordStats[selectedWeapons].Defend);
            PlayerPrefs.SetInt("currentWeaponHP", swordStats[selectedWeapons].Health);
            PlayerPrefs.SetInt("currentWeaponEM", swordStats[selectedWeapons].EM);
        }
        else if (Claymore)
        {
            PlayerPrefs.SetString("weaponType", "claymore");
            PlayerPrefs.SetInt("currentWeaponATK", claymoreStats[selectedWeapons].Attack);
            PlayerPrefs.SetInt("currentWeaponDEF", claymoreStats[selectedWeapons].Defend);
            PlayerPrefs.SetInt("currentWeaponHP", claymoreStats[selectedWeapons].Health);
            PlayerPrefs.SetInt("currentWeaponEM", claymoreStats[selectedWeapons].EM);
        }
        else
        {
            PlayerPrefs.SetString("weaponType", "catalyst");
            PlayerPrefs.SetInt("currentWeaponATK", catalystStats[selectedWeapons].Attack);
            PlayerPrefs.SetInt("currentWeaponDEF", catalystStats[selectedWeapons].Defend);
            PlayerPrefs.SetInt("currentWeaponHP", catalystStats[selectedWeapons].Health);
            PlayerPrefs.SetInt("currentWeaponEM", catalystStats[selectedWeapons].EM);
        }
        SceneManager.LoadScene("SelectCharacterScene");
    }
    public void ResetSelectedNumber()
    {
        swords[selectedWeapons].SetActive(false);
        claymore[selectedWeapons].SetActive(false);
        catalyst[selectedWeapons].SetActive(false);
        selectedWeapons = 0;
        swords[selectedWeapons].SetActive(true);
        claymore[selectedWeapons].SetActive(true);
        catalyst[selectedWeapons].SetActive(true);
    }
    public void WeaponUI()
    {
        if (Sword == true)
        {
            if (idList.Contains(swordStats[selectedWeapons].WeaponId))
            {
                swordStats[selectedWeapons].isUnlock = true;
                unlockButton.gameObject.SetActive(false);
            }
            else
            {
                swordStats[selectedWeapons].isUnlock = false;
                unlockButton.gameObject.SetActive(true);
            }
            name.text = swordStats[selectedWeapons].Name;
            attack.text = " + " + swordStats[selectedWeapons].Attack.ToString();
            defend.text = " + " + swordStats[selectedWeapons].Defend.ToString();
            health.text = " + " + swordStats[selectedWeapons].Health.ToString();
            elementsMastery.text = " + " + swordStats[selectedWeapons].EM.ToString();
            selectButton.SetActive(swordStats[selectedWeapons].isUnlock);
        }
        else if (Claymore == true)
        {
            if (idList.Contains(claymoreStats[selectedWeapons].WeaponId))
            {
                claymoreStats[selectedWeapons].isUnlock = true;
                unlockButton.gameObject.SetActive(false);
            }
            else
            {
                claymoreStats[selectedWeapons].isUnlock = false;
                unlockButton.gameObject.SetActive(true);
            }
            name.text = claymoreStats[selectedWeapons].name;
            attack.text = " + " + claymoreStats[selectedWeapons].Attack.ToString();
            defend.text = " + " + claymoreStats[selectedWeapons].Defend.ToString();
            health.text = " + " + claymoreStats[selectedWeapons].Health.ToString();
            elementsMastery.text = " + " + claymoreStats[selectedWeapons].EM.ToString();
            selectButton.SetActive(claymoreStats[selectedWeapons].isUnlock);
        }
        else
        {
            if (idList.Contains(catalystStats[selectedWeapons].WeaponId))
            {
                catalystStats[selectedWeapons].isUnlock = true;
                unlockButton.gameObject.SetActive(false);
            }
            else
            {
                catalystStats[selectedWeapons].isUnlock = false;
                unlockButton.gameObject.SetActive(true);
            }
            name.text = catalystStats[selectedWeapons].name;
            attack.text = " + " + catalystStats[selectedWeapons].Attack.ToString();
            defend.text = " + " + catalystStats[selectedWeapons].Defend.ToString();
            health.text = " + " + catalystStats[selectedWeapons].Health.ToString();
            elementsMastery.text = " + " + catalystStats[selectedWeapons].EM.ToString();
            selectButton.SetActive(catalystStats[selectedWeapons].isUnlock);
        }
    }
    public void WeaponUnlock()
    {
        if(GameManager.Instance.userWT >= ticket)
        {
            GameManager.Instance.SpendWeaponTicket(ticket);
            unlockButton.gameObject.SetActive(false);
            selectButton.SetActive(true);
        }
        if (Sword == true)
        {
            swordStats[selectedWeapons].isUnlock = true;
            idList.Add(swordStats[selectedWeapons].WeaponId);
        }
        else if (Claymore == true)
        {
            claymoreStats[selectedWeapons].isUnlock = true;
            idList.Add(claymoreStats[selectedWeapons].WeaponId);
        }
        else
        {
            catalystStats[selectedWeapons].isUnlock = true;
            idList.Add(catalystStats[selectedWeapons].WeaponId);
        }
        saveItem.SaveIdList(idList);
    }

    public void SetWeaponTicket()
    {
        if (Sword == true)
        {
            ticket = swordStats[selectedWeapons].TicketNeed = (int)swordStats[selectedWeapons].Rare + 1;
        }
        else if (Claymore == true)
        {
            ticket = claymoreStats[selectedWeapons].TicketNeed = (int)claymoreStats[selectedWeapons].Rare + 1;
        }
        else
        {
            ticket = catalystStats[selectedWeapons].TicketNeed = (int)catalystStats[selectedWeapons].Rare + 1;
        }
        ticketUI.text = ticket.ToString();
        if(GameManager.Instance.userWT < ticket)
        {
            ticketUI.color = Color.red;
        }
        else
        {
            ticketUI.color = Color.green;
        }
    }
    public void ResetWeapon()
    {
        if (Sword == true)
        {
            swordStats[selectedWeapons].isUnlock = false;
            idList.Remove(swordStats[selectedWeapons].WeaponId);
        }
        else if (Claymore == true)
        {
            claymoreStats[selectedWeapons].isUnlock = false;
            idList.Remove(claymoreStats[selectedWeapons].WeaponId);
        }
        else
        {
            catalystStats[selectedWeapons].isUnlock = false;
            idList.Remove(catalystStats[selectedWeapons].WeaponId);
        }
        Debug.Log("Remove Success");
    }
    private void Start()
    {
        SetDefault();
        saveItem = GetComponent<SaveItemManager>();
        idList = saveItem.LoadIdList();
    }

    private void Update()
    {
        WeaponUI();
        SetWeaponTicket();
    }

    
    
}
