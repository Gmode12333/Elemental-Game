using System;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Character" , menuName = "Character")]
public class CharacterProfile : ScriptableObject
{
    [Header("Character Star")]
    public CharacterRare Rare;
    [Header("Character Info")]
    public string Name;
    public int Level;
    public int TicketRequire;
    public bool IsUnlock;
    public bool CanUnlock;
    public string Sex;
    [Header("Character Buff")]
    public int Attack;
    public int Defend;
    public int MaxHealth;
    public int ElementsMastery;

    public void SaveInfo(int atk, int def, int hp, int em)
    {
        PlayerPrefs.SetInt(Name + "Attack", atk);
        PlayerPrefs.SetInt(Name + "Defend", def);
        PlayerPrefs.SetInt(Name + "Health", hp);
        PlayerPrefs.SetInt(Name + "EM", em);
    }
}
[Serializable]
public enum CharacterRare
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}
