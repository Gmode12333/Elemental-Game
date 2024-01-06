using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFollow : MonoBehaviour
{
    public Transform followTarget;
    public float speed;

    private void Start()
    {
        followTarget = GameObject.FindGameObjectWithTag("Weapon").transform;
    }
    private void Update()
    {
        WeaponSlowlyFollow();
    }
    public void WeaponSlowlyFollow()
    {
        transform.position = Vector3.Lerp(transform.position, followTarget.position, speed * Time.deltaTime);
    }
}
