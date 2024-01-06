using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    private float minChance = 0.01f;
    private float maxChance = 1f;
    private void OnTriggerEnter(Collider other)
    {
        float isCrit = Random.Range(minChance, maxChance);
        if(isCrit <= InGameManager.Instance.playerCritRate)
        {
            if (other.CompareTag("Enemy") && InGameManager.Instance.isPlayerAttacking)
            {
                other.GetComponent<HittedObject>().TakeDamage(InGameManager.Instance.playerATK * (3 * InGameManager.Instance.playerCritDamage + 1));
            }
        }
        else
        {
            if (other.CompareTag("Enemy") && InGameManager.Instance.isPlayerAttacking)
            {
                other.GetComponent<HittedObject>().TakeDamage(InGameManager.Instance.playerATK);
            }
        }
    }
}
