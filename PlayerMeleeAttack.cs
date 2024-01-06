using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    [Header("Combo Attack")]
    public string[] animationCombo;
    private float attackTime;
    private float attackDelay;
    private int currentCombo;
    public bool isAttacking;
    [Header("Attack Info")]
    [SerializeField] LayerMask Target;
    [SerializeField] Transform rangeAttackPoint;
    //Input && anim
    [Header("Weapon Info")]
    public Transform SpawnPoint;
    public GameObject[] swordSlash;
    public GameObject[] claymoreSlash;
    public GameObject catalystSlash;
    public GameObject bullet;

    private StarterAssetsInputs input;
    private ThirdPersonController controller;
    private Animator anim;
    private AimSCript aim;
    private float cooldown;
    private string weaponType;
    private void Start()
    {
        aim = GetComponent<AimSCript>();
        input = GetComponent<StarterAssetsInputs>();
        anim = GetComponent<Animator>();
        controller = GetComponent<ThirdPersonController>();
        weaponType = PlayerPrefs.GetString("weaponType");
        if (weaponType == "sword")
        {
            attackTime = 1.25f * InGameManager.Instance.playerAttackSpeed;
        }
        else if (weaponType == "claymore")
        {
            attackTime = 0.75f * InGameManager.Instance.playerAttackSpeed;
        }
        else
        {
            attackTime = 1.25f * InGameManager.Instance.playerAttackSpeed;
        }
    }
    private void Update()
    {
        if (input.shoot && Time.time >= attackDelay)
        {
            anim.Play(animationCombo[currentCombo], InGameManager.Instance.WeaponAnimation());
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
        if(Time.time >= cooldown)
        {
            InGameManager.Instance.isPlayerAttacking = false;
            isAttacking = false;
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
        InGameManager.Instance.isPlayerAttacking = true;

        isAttacking = true;
        if (weaponType == "catalyst")
        {
            StartCoroutine(CataLystShoot());   
            ActiveSlash();
            AttackCoolDown();
        }
        else
        {
            ActiveSlash();
            AttackCoolDown();
        }
    }
    public void ActiveSlash()
    {
        if (weaponType == "sword")
        {
            swordSlash[currentCombo].SetActive(true);
            SoundManager.Instance.PlaySFX("Sword" + (currentCombo + 1));
            StartCoroutine(ActiveTime(swordSlash[currentCombo]));
        }
        else if (weaponType == "claymore")
        {
            claymoreSlash[currentCombo].SetActive(true);
            SoundManager.Instance.PlaySFX("Claymore" + (currentCombo + 1));
            StartCoroutine(ActiveTime(claymoreSlash[currentCombo]));
        }
        else if (weaponType == "catalyst")
        {
            catalystSlash.SetActive(true);
            SoundManager.Instance.PlaySFX("Catalyst");
            StartCoroutine(ActiveTime(catalystSlash));
        }
    }
    public float AttackCoolDown()
    {
        cooldown = Time.time + 1f / attackTime;
        return cooldown;
    }
    IEnumerator ActiveTime(GameObject obj)
    {
        controller.MoveSpeed = 1;
        controller.SprintSpeed = 1;
        yield return new WaitForSeconds(1f);
        obj.SetActive(false);
        controller.MoveSpeed = 5;
        controller.SprintSpeed = 9;
    }
    IEnumerator CataLystShoot()
    {
        int time = 0;
        while (time < 2)
        {
            yield return new WaitForSeconds(0.1f);
            Vector3 aimDir = (aim.shootDir - rangeAttackPoint.position).normalized;
            Instantiate(bullet, rangeAttackPoint.position, Quaternion.LookRotation(aimDir, Vector3.up));
            time++;
        }
    }
}
