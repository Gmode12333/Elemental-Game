using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBuff : MonoBehaviour
{
    public int healthBuff;
    public int damageBuff;
    public int bossAttackSpeedBuff;

    private void Start()
    {
        InGameManager.Instance.EnemyHealth += healthBuff;
        InGameManager.Instance.EnemyRangeDamage += damageBuff;
        InGameManager.Instance.EnemyAttackSpeed += bossAttackSpeedBuff;
    }

}
