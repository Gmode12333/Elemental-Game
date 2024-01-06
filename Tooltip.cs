using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler
{
    [SerializeField] string header;
    [SerializeField] string content;
    [SerializeField] string skillPoints;
    public bool isOpen;
    private Skill skill;
    private void Start()
    {
        skill = GetComponent<Skill>();
        if (skill.isPassive)
        {
            content = skill.PassiveContent().ToString();
        }
        else if (skill.isActive)
        {
            content = skill.ActiveContent().ToString();
        }
        header = skill.skillName();
        skillPoints = skill.SkillPoints();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.Show(content, header, skillPoints);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }
}
