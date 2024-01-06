using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InGameManager : GlobalReference<InGameManager>
{
    [Header("Player && Character")]
    public GameObject[] characters;
    public Transform parent;
    public bool isPlayerAttacking;
    public string characterName;

    public int playerTotalHealth;
    public int playerCurrentHealth;
    public int playerATK;
    public int playerDEF;
    public int playerEM;
    public float playerAttackSpeed;
    public float playerCritDamage;
    public float playerCritRate;
    public float playerRegen;
    public float playerMoveSpeed;

    [Header("Enemy")]
    public int EnemyMeleeDamage;
    public int EnemyRangeDamage;
    public float EnemyAttackSpeed;
    public int EnemyHealth;

    [Header("Weapons GameObject")]
    public Transform weaponIdle;
    [SerializeField] GameObject[] swords;
    [SerializeField] GameObject[] claymore;
    [SerializeField] GameObject[] catalyst;
    public Transform weaponTransform;
    [Header("UI && Object")]
    public UIHealth health;
    public GameObject PauseMenu;
    public LevelManager levelManager;
    public bool isPaused;
    [Header("Level")]
    public int currentLevel;
    public int totalGem;
    public int totalExp;
    public int currentAliveEnemy;
    public bool isOnState;
    public bool isLose;

    private int characterIndex;
    private string weaponType;
    private int weaponTypeNum;

    private GameObject weaponInHand;
    private GameObject weaponOutHand;
    protected override void Awake()
    {
        base.Awake();
        PlayerInGameStats();
        SetActiveCharacter();
        SetWeapon();
        SetLevel();
        Cursor.visible = false;
        Debug.Log(weaponTypeNum);
    }
    private void Update()
    {
        if (isPlayerAttacking && playerCurrentHealth > 0)
        {
            SetWeaponDissolveOn();
        }
        else if (!isPlayerAttacking && playerCurrentHealth > 0)
        {
            SetWeaponDissolveOff();
        }
        currentAliveEnemy = EnemyAI.EnemyCount;
        if(Input.GetKeyDown(KeyCode.Escape) && !isLose)
        {
            PauseGame();
        }
        if(playerCurrentHealth <= 0 && !isLose)
        {
            isLose = true;
            Cursor.visible = true;
            SetLose();
        }
    }
    public void PauseGame()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0;
            PauseMenu.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1;
            PauseMenu.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void SetActiveCharacter()
    {
        characterIndex = PlayerPrefs.GetInt("selectedCharacter");
        var character = Instantiate(characters[characterIndex]);
        characterName = characters[characterIndex].name;
        character.transform.SetParent(parent, false);
    }
    public void GoBackToCharacter()
    {
        SceneManager.LoadScene("SelectCharacterScene");
    }
    public void SetWeapon()
    {
        int index = PlayerPrefs.GetInt("selectedWeapon");
        weaponType = PlayerPrefs.GetString("weaponType");
        if (weaponType == "sword")
        {
            weaponInHand = Instantiate(swords[index], weaponTransform);
            weaponOutHand = Instantiate(swords[index], weaponIdle);
            weaponTypeNum = 1;
        }
        else if(weaponType == "claymore")
        {
            weaponInHand = Instantiate(claymore[index], weaponTransform);
            weaponOutHand = Instantiate(claymore[index], weaponIdle);
            weaponTypeNum = 2;
        }
        else
        {
            weaponInHand = Instantiate(catalyst[index], weaponTransform);
            weaponOutHand = Instantiate(catalyst[index], weaponIdle);
            weaponTypeNum = 3;
        }
    }
    public void SetWeaponDissolveOn()
    {
        weaponOutHand.GetComponent<DissolveManager>().Dissolve();
        weaponInHand.GetComponent<DissolveManager>().ReturnToNormal();
    }
    public void SetWeaponDissolveOff()
    {
        weaponOutHand.GetComponent<DissolveManager>().ReturnToNormal();
        weaponInHand.GetComponent<DissolveManager>().Dissolve();
    }
    public int WeaponAnimation()
    {
        return weaponTypeNum;
    }
    public int UpdateHealth(int value)
    {
        playerCurrentHealth -= value;
        health.UpdateUI();
        return playerCurrentHealth;
    }
    public void PlayerInGameStats()
    {
        playerTotalHealth = GameManager.Instance.CHP + GameManager.Instance.weaponHP + GameManager.Instance.skillHP;
        playerCurrentHealth = playerTotalHealth;

        playerATK = GameManager.Instance.CATK + GameManager.Instance.weaponATK + GameManager.Instance.skillATK;
        playerDEF = GameManager.Instance.CDEF + GameManager.Instance.weaponDEF + GameManager.Instance.skillDEF;
        playerEM = GameManager.Instance.CEM + GameManager.Instance.weaponEM + GameManager.Instance.skillEM;

        playerAttackSpeed = 1 + GameManager.Instance.skillASPD / 100f;
        playerCritDamage = GameManager.Instance.skillCD / 100f;
        playerCritRate = GameManager.Instance.skillCR / 100f;
        playerRegen = GameManager.Instance.skillRegen / 100f;
        playerMoveSpeed = GameManager.Instance.skillMSPD / 100f;
    }
    public void SetEnemyStats()
    {
        EnemyMeleeDamage = currentLevel * 25;
        EnemyRangeDamage = currentLevel * 15;
        EnemyAttackSpeed = 1f * currentLevel / 100;
        EnemyHealth = currentLevel * 500;
    }
    public void SetLevel()
    {
        currentLevel = PlayerPrefs.GetInt("currentLevel") + 1;
        totalGem = (currentLevel * 25) * 2;
        totalExp = currentLevel * 100;
        EnemyAttackSpeed = 1f + currentLevel / 100;
        EnemyHealth = currentLevel * 150 + 350;
        EnemyRangeDamage = currentLevel * 10 + 10;
        EnemyMeleeDamage = currentLevel * 20 + 10;
    }
    public void SetWin()
    {
        SoundManager.Instance.PlaySFX("Win");
        if(currentLevel == 10)
        {
            levelManager.gameObject.SetActive(true);
            levelManager.CompleteMission();
            levelManager.StartTimeToEndCutScene();
            GameManager.Instance.userCT += 5;
            GameManager.Instance.userWT += 5;
            PlayerPrefs.SetInt("currentLevel", currentLevel);
            GameManager.Instance.SaveGameData();
        }
        else
        {
            levelManager.gameObject.SetActive(true);
            levelManager.StartTimeReturnHome();
            levelManager.WinText();
            GameManager.Instance.userCT += 5;
            GameManager.Instance.userWT += 5;
            PlayerPrefs.SetInt("currentLevel", currentLevel);
            GameManager.Instance.SaveGameData();
        }
    }
    public void SetLose()
    {
        SoundManager.Instance.PlaySFX("Lose");
        GameManager.Instance.userCT += 1;
        GameManager.Instance.userWT += 1;
        levelManager.gameObject.SetActive(true);
        levelManager.StartTimeReturnHome();
        levelManager.LoseText();
        GameManager.Instance.SaveGameData();
    }
}
