using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    [SerializeField] int enemyMeleeDamage;
    [SerializeField] float enemyMeleeSpeed;
    private EnemyAI enemyAI;
    private Animator anim;
    private void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        enemyMeleeDamage = InGameManager.Instance.EnemyMeleeDamage;
        enemyMeleeSpeed = InGameManager.Instance.EnemyAttackSpeed;
        anim = GetComponent<Animator>();
        anim.SetFloat("AttackSpeed", enemyMeleeSpeed);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && enemyAI.isAttack)
        {
            other.GetComponent<ThirdPersonHealth>().TakeDamage(enemyMeleeDamage);
        }
    }
}
