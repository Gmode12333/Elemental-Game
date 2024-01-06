using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorOverTime : MonoBehaviour
{
    [SerializeField][Range(0f, 1f)] float LerpTime;
    [SerializeField] Color[] color;

    private TextMeshProUGUI text;
    private int colorIndex = 0;
    private float t = 0;
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        text.color = Color.Lerp(text.color, color[colorIndex], LerpTime * Time.deltaTime);

        t = Mathf.Lerp(t, 1f, LerpTime * Time.deltaTime);
        if(t > 0.9f)
        {
            t = 0f;
            colorIndex++;
            colorIndex = (colorIndex >= color.Length) ? 0 : colorIndex;
        }
    }
}
