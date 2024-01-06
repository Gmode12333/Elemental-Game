using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<EffectOnTarget>().ApplyDamageEffect(1f, 0.5f, 100);
            other.GetComponent<EffectOnTarget>().ApplyMovingEffect("Slow", 1f);
        }
    }
    
}
