using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamagePopup : MonoBehaviour
{
    public float destroyTime = 3.0f;
    public Vector3 offset = new Vector3 (0, 2, 0);

    private void Start()
    {

        Destroy(gameObject, destroyTime);
        transform.localPosition += offset;
    }
    private void LateUpdate()
    {
        var cameraToLookAt = Camera.main;
        transform.LookAt(cameraToLookAt.transform);
        transform.rotation = Quaternion.LookRotation(cameraToLookAt.transform.forward);
    }
}
