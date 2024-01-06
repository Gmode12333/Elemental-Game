using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnParticalCollision : MonoBehaviour
{
    private ActiveSkill skillInfo;
    private float minChance = 0.01f;
    private float maxChance = 1f;
    private void Start()
    {
        skillInfo = GetComponent<ActiveSkill>();
    }
    private void OnTriggerEnter(Collider other)
    {
        float isCrit = Random.Range(minChance, maxChance);
        if(isCrit <= InGameManager.Instance.playerCritRate)
        {
            if (other.CompareTag("Enemy"))
            {
                float skillDamage = (skillInfo.skillDamage + GameManager.Instance.skillEM + GameManager.Instance.CEM) * (3 * InGameManager.Instance.playerCritDamage + 1);
                other.GetComponent<HittedObject>().TakeDamage(skillDamage);
                if (skillInfo.effectType == SkillEffect.None)
                {
                    return;
                }
                if (skillInfo.effectType == SkillEffect.Freeze || skillInfo.effectType == SkillEffect.Stun)
                {
                    other.GetComponent<EffectOnTarget>().ApplyMovingEffect("Stop", skillInfo.skillDuration);
                }
                else
                {
                    other.GetComponent<EffectOnTarget>().ApplyMovingEffect("Slow", skillInfo.skillDuration);
                    other.GetComponent<EffectOnTarget>().ApplyDamageEffect(skillInfo.skillDuration, skillInfo.skillEffectTime, (int)skillDamage);
                }
            }
        }
        else if(other.CompareTag("Enemy"))
        {
            float skillDamage = skillInfo.skillDamage + GameManager.Instance.skillEM + GameManager.Instance.CEM;
            other.GetComponent<HittedObject>().TakeDamage(skillDamage);
            if (skillInfo.effectType == SkillEffect.None)
            {
                return;
            }
            if (skillInfo.effectType == SkillEffect.Freeze || skillInfo.effectType == SkillEffect.Stun)
            {
                other.GetComponent<EffectOnTarget>().ApplyMovingEffect("Stop", skillInfo.skillDuration);
            }
            else
            {
                other.GetComponent<EffectOnTarget>().ApplyMovingEffect("Slow", skillInfo.skillDuration);
                other.GetComponent<EffectOnTarget>().ApplyDamageEffect(skillInfo.skillDuration, skillInfo.skillEffectTime, (int)skillDamage);
            }
        }
    }
}
