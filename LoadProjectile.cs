using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadProjectile : MonoBehaviour
{
    [SerializeField] GameObject[] projectilePrefabs;
    private PlayerRangeAttack projectileSelect;
    private void Start()
    {
        int selected = PlayerPrefs.GetInt("selectedElement");
        projectileSelect = GetComponentInParent<PlayerRangeAttack>();
    }
}
