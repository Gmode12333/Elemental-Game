using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EasyUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject UIObject;
    public void OnPointerEnter(PointerEventData eventData)
    {
        UIObject.transform.position = transform.position + new Vector3(20 , -20 , 0);
        UIObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIObject.SetActive(false);
    }
}
