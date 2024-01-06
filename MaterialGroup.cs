using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MaterialGroup : MonoBehaviour
{
    List<Material> materials = new List<Material>();

    [SerializeField]
    private Renderer[] renderers;

    private void Start()
    {
        foreach (Renderer renderer in renderers)
        {
            materials.AddRange(renderer.materials);
        }
    }

    public void SetColor(Color color)
    {
        foreach (Material material in materials)
        {
            material.color = color;
        }
    }

    public void SetColor(Color color, float time)
    {
        SetColor(color);
        StopAllCoroutines();
        StartCoroutine(ResetColor(time));
    }

    public void ModifyMaterials(Action<Material> changeAction)
    {
        foreach (Material material in materials)
        {
            changeAction(material);
        }
    }

    [ContextMenu("Get Renderer Components")]
    private void GetRendererComponents()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    IEnumerator ResetColor(float time)
    {
        yield return new WaitForSeconds(time);
        SetColor(Color.white);
    }
}
