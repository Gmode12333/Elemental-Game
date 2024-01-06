using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPosition : MonoBehaviour
{
    public Transform MeleePosition;
    private void Awake()
    {
        InGameManager.Instance.weaponTransform = MeleePosition;
    }
}
