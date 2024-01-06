using System.Collections;
using UnityEngine;

public class DissolveManager : MonoBehaviour
{
    public Material dissolveMaterial;
    private bool isDissolve;
    private string weaponType;
    private float offset;
    private void Awake()
    {
        dissolveMaterial = GetComponentInChildren<Renderer>().material;
        GetWeaponType();
    }
    public void Dissolve()
    {
        StartCoroutine(DissolveCoroutine());
    }
    public void ReturnToNormal()
    {
        if (isDissolve)
        {
            StartCoroutine(ReturnToNormalCoroutine());
        }
    }

    public void GetWeaponType()
    {
        weaponType = PlayerPrefs.GetString("weaponType");
        if(weaponType == "claymore")
        {
            offset = 1.5f;
        }
        else
        {
            offset = 1;
        }
    }
    private Vector3 GetVector3Value(Material mat)
    {
        Vector3 value = Vector3.zero;
        if(mat != null)
        {
            value = mat.GetVector("_DissolveOffest");
        }
        return value;
    }
    IEnumerator DissolveCoroutine()
    {
        Vector3 nodeValue = GetVector3Value(dissolveMaterial);
        while(nodeValue.z > -offset)
        {
            nodeValue.z -= 0.2f;
            dissolveMaterial.SetVector("_DissolveOffest", nodeValue);
            isDissolve = true;
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator ReturnToNormalCoroutine()
    {
        Vector3 nodeValue = GetVector3Value(dissolveMaterial);
        while(nodeValue.z < offset)
        {
            nodeValue.z += 0.2f;
            dissolveMaterial.SetVector("_DissolveOffest", nodeValue);
            isDissolve = false;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
