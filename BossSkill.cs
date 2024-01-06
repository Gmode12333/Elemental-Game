using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BossSkill : MonoBehaviour
{
    public GameObject AttackSkill;
    public GameObject HealSkill;
    public GameObject DefendSkill;
    public Transform AttackPoints;

    private HittedObject hit;
    private EnemyAI enemyAI;
    private Animator anim;
    private EffectOnTarget effect;
    [SerializeField] float skillCoolDown;
    public float NextSkillTime;

    private void Start()
    {
        hit = GetComponent<HittedObject>();
        enemyAI = GetComponent<EnemyAI>();
        anim = GetComponent<Animator>();
        effect = GetComponent<EffectOnTarget>();
    }
    private void Update()
    {
        if (hit.Health <= 0 || effect.isOnEffect)
            return;
        float distance = Vector3.Distance(transform.position, enemyAI.target.transform.position);
        if (hit.Health <= 500)
        {
            NextSkillTime = 1f;
        }
        else
        {
            NextSkillTime = 4f;
        }
        if (distance <= 3f)
        {
            SpawnDefendSkill();
        }
        if (distance <= 10f)
        {
            SpawnAttackSkill();
        }
        if(distance > 10f)
        {
            SpawnHealSkill();
        }
    }
    public void SpawnAttackSkill()
    {
        if(skillCoolDown > Time.time)
        {
            return;
        }
        anim.Play("EnemySkill");
        AttackSkill.gameObject.SetActive(true);
        StartCoroutine(SkillTime(AttackSkill));
    }
    public void SpawnHealSkill()
    {
        if (skillCoolDown > Time.time)
        {
            return;
        }
        HealSkill.gameObject.SetActive(true);
        anim.Play("Healing");
        hit.Health += 250;
        StartCoroutine(SkillTime(HealSkill));
    }
    public void SpawnDefendSkill()
    {
        if (skillCoolDown > Time.time)
        {
            return;
        }
        DefendSkill.gameObject.SetActive(true);
        StartCoroutine(SkillTime(DefendSkill));
    }
    IEnumerator SkillTime(GameObject obj)
    {
        skillCoolDown = Time.time + NextSkillTime;
        yield return new WaitForSeconds(2f);
        obj.SetActive(false);
        anim.SetBool("IsHealing", false);
    }
}
