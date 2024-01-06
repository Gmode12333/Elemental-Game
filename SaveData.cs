
using UnityEngine;

[CreateAssetMenu(fileName = "New Save Data", menuName = "Save Data")]
public class SaveData : ScriptableObject
{
    public string CharacterName;
    public int CharacterLevel;
    public int CharacterAttack;
    public int CharacterDefend;
    public int CharacterMaxHealth;
    public int CharacterEM;
}
