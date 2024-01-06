using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class PlayerAttack : MonoBehaviour
{
    [Header("Combo Attack")]
    public string[] animationCombo;
    public float attackTime;
    public float attackDelay;
    public int currentCombo;
    [Header("Attack Info")]
    [SerializeField] float attackRange;
    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask Target;
    //Input && anim
    [Header("Weapon Info")]
    public Transform SpawnPoint;
    public GameObject[] swordSlash;
    public GameObject[] claymoreSlash;

    private StarterAssetsInputs input;
    private Animator anim;
    private string weaponType;
    private void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
        anim = GetComponent<Animator>();
        weaponType = PlayerPrefs.GetString("weaponType");
        if (weaponType == "sword")
        {
            attackRange = 1.5f;
            attackTime = 1.25f;
        }
        else if (weaponType == "claymore")
        {
            attackRange = 2.5f;
            attackTime = 0.75f;
        }
    }
    private void Update()
    {
        if (input.shoot && Time.time >= attackDelay)
        {
            anim.Play(animationCombo[currentCombo],InGameManager.Instance.WeaponAnimation());
            anim.SetLayerWeight(InGameManager.Instance.WeaponAnimation(), 1);
            Attack();
            NextAttack();
            currentCombo++;
            if (currentCombo == animationCombo.Length)
            {
                ResetCombo();
            }
            input.shoot = false;
        }
    }
    private void ResetCombo()
    {
        currentCombo = 0;
    }
    private float NextAttack()
    {
        attackDelay = Time.time + 1f / attackTime;
        return attackDelay;
    }
    public void Attack()
    {
        Collider[] hits = Physics.OverlapSphere(attackPoint.position, attackRange, Target);
        ActiveSlash();
        foreach (Collider hit in hits)
        {
            if(hit.TryGetComponent<HittedObject>(out var obj))
            {
                obj.TakeDamage(InGameManager.Instance.playerATK);
                Debug.Log("Hits");
            }
        }
    }
    public void ActiveSlash()
    {
        if (weaponType == "sword")
        {
            swordSlash[currentCombo].SetActive(true);
            StartCoroutine(ActiveTime(swordSlash[currentCombo]));
        }
        else if (weaponType == "claymore")
        {
            claymoreSlash[currentCombo].SetActive(true);
            StartCoroutine(ActiveTime(claymoreSlash[currentCombo]));
        }
        else
        {
            
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    IEnumerator ActiveTime(GameObject obj)
    {
        yield return new WaitForSeconds(1f);
        obj.SetActive(false);
    }
}
