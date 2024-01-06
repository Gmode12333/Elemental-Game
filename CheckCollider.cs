using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollider : MonoBehaviour
{
    public LayerMask MapMask;
    public bool isDashAble = false;
    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, 5f, MapMask))
        {
            isDashAble = true;
        }
        else
        {
            isDashAble = false;
        }
    }
}
