using System;
using System.Collections;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    public static int EnemyCount {  get; private set; }

    [Header("Enemy Combat Setting")]
    public float enemyAttackRange;
    public float enemyDetectRange;
    public float enemyMoveSpeed;

    [Header("Target")]
    public GameObject target;
    public bool isInRange = false;
    public bool isAttack = false;
    private NavMeshAgent enemy;
    private Animator anim;
    private HittedObject hit;
    private BossSkill bossSkill;

    [Header("Enemy Random Patrol")]
    [SerializeField] float walkRange;
    [SerializeField] float walkSpeed;
    [SerializeField] float timeToChangeDestination;
    [SerializeField] Vector3 RandomPoint;

    private void Start()
    {
        anim = GetComponent<Animator>();
        enemy = GetComponent<NavMeshAgent>();
        hit = GetComponent<HittedObject>();
        target = GameObject.FindWithTag("Player");
        Patrol();
    }

    private void OnEnable()
    {
        EnemyCount++;
    }

    private void OnDisable()
    {
        EnemyCount--;
    }

    private void Update()
    {
        EnemyInCombat();
    }
    private void LookAtTarget()
    {
        Vector3 lookPos = target.transform.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
    }
    private void EnemyInCombat()
    {
        float inRange = Vector3.Distance(target.transform.position, transform.position);
        if(inRange <= enemyDetectRange)
        {
            isInRange = true;
            isAttack = false;
            enemy.SetDestination(target.transform.position);
            enemy.speed = enemyMoveSpeed;
            anim.SetFloat("Speed", enemyMoveSpeed);
            if(inRange <= enemyAttackRange && hit.isHit != true)
            {
                enemy.speed = 0;
                isAttack = true;
                anim.Play("Attack");
                LookAtTarget();
            }
        }
        else if(isInRange && inRange > enemyDetectRange)
        {
            isInRange = false;
            StartCoroutine(ChangeDestination());
            isAttack = false;
        }
    }
    private void Patrol()
    {
        StartCoroutine(ChangeDestination());
    }
    private void SetRandomDestination()
    {
        float x = Random.Range(-walkRange, walkRange);
        float z = Random.Range(-walkRange, walkRange);

        RandomPoint = new Vector3(transform.position.x + x, 0, transform.position.z + z);
    }
    private void MoveToDestination()
    {
        if(transform.position.x != RandomPoint.x)
        {
            enemy.SetDestination(RandomPoint);
            enemy.speed = walkSpeed;
            anim.SetFloat("Speed", walkSpeed);
        }
        else
        {
            enemy.speed = 0;
            anim.SetFloat("Speed", 0);
        }
    }
    IEnumerator ChangeDestination()
    {
        while (isInRange == false)
        {
            SetRandomDestination();
            MoveToDestination();
            yield return new WaitForSeconds(timeToChangeDestination);
        }
    }
}
