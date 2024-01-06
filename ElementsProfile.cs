using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Elements", menuName = "ElementBuffs")]
public class ElementsProfile : ScriptableObject
{
    [Header("Elements")]
    public Categories elements;
    [Header("Elements Buff")]
    public int fireBuff;
    public int waterBuff;
    public int lightningBuff;
    public int poisonBuff;
    [Header("Elements Damaged")]
    public int elementsDamage;
    public int skillDamage;


    public void CalculateDamage()
    {
        int total = fireBuff % 100 + waterBuff % 100 + lightningBuff % 100 + poisonBuff % 100;
        elementsDamage = total;
        skillDamage = elementsDamage * 2;
    }
}
public enum Categories
{
    Fire,
    Water,
    Lightning,
    Poison,
}
