using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LoadElements : MonoBehaviour
{
    [SerializeField] GameObject[] elementPrefabs;
    private ThirdPersonPrepareShooting elementSelect;
    private void Start()
    {
        int selected = PlayerPrefs.GetInt("selectedElement");
        elementSelect = GetComponentInParent<ThirdPersonPrepareShooting>();
        elementSelect.pfElement = elementPrefabs[selected];
    }
}
