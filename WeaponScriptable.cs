using System.Security.Cryptography;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapons", menuName = "WeaponStats")]
public class WeaponScriptable : ScriptableObject
{
    [Header("Weapon Star")]
    public WeaponRare Rare;
    [Header("Weapon Info")]
    public int WeaponId;
    public string Name;
    public WeaponType WeaponType;
    public int TicketNeed;
    public bool isUnlock;
    [Header("Weapon Stats")]
    public int Attack;
    public int Defend;
    public int Health;
    public int EM;
}

public enum WeaponType
{
    Sword,
    Claymore,
    Catalyst
}
public enum WeaponRare
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}
