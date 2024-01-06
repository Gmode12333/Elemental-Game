using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem current;

    public TooltipTrigger tooltipTrigger;
    private void Awake()
    {
        current = this;
    }
    public static void Show(string content, string header, string points)
    {
        current.tooltipTrigger.SetText(content, header, points);
        current.tooltipTrigger.gameObject.SetActive(true);
    }
    public static void Hide()
    {
        current.tooltipTrigger.gameObject.SetActive(false);
    }
}
