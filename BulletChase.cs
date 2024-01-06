using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletChase : MonoBehaviour
{
    private Transform target;
    //make bullet chasae enemy
    private void Start()
    {
        IsHadTarget();
    }
    public void IsHadTarget()
    {
        if(EnemyAI.EnemyCount != 0)
        {
            target.position = GameObject.FindGameObjectWithTag("Enemy").transform.position;
            transform.LookAt(target);
            Vector3 targetPos = (target.position - transform.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 20 * Time.deltaTime);
        }
        else
        {
            return;
        }
    }
}
