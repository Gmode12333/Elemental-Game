using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    [Header("Enemy Range")]
    [SerializeField] int enemyAttackDamage;
    [SerializeField] float enemyAttackSpeed;
    [Header("Enemy Projectile")]
    public Transform spawnPoint;
    public GameObject projectilePF;
    private EnemyAI enemyAI;
    private Animator anim;
    private void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        anim = GetComponent<Animator>();
        enemyAttackDamage = InGameManager.Instance.EnemyRangeDamage;
        enemyAttackSpeed = InGameManager.Instance.EnemyAttackSpeed;
        anim.SetFloat("AttackSpeed", enemyAttackSpeed);
    }
    public void EnemyRangeAttack()
    {

        Instantiate(projectilePF, spawnPoint);

    }
}
