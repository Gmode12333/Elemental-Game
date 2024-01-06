using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacter : MonoBehaviour
{
    [SerializeField] GameObject[] characterPrefabs;
    private void Start()
    {
        int selected = PlayerPrefs.GetInt("selectedCharacter");
        characterPrefabs[selected].SetActive(true);
    }
}
