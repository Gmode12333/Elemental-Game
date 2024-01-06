using StarterAssets;
using System.Collections;
using UnityEngine;

public class PlayerSkillEffect : MonoBehaviour
{
    public Transform skillPosition;

    private ThirdPersonController playerController;
    private PlayerMeleeAttack playerMeleeAttack;
    private Animator anim;
    public CheckCollider checkCollider;

    [Header("Dash Info")]
    [SerializeField] GameObject dashEffect;
    [SerializeField] float dashSpeed = 15f;
    [SerializeField] float dashTime = 0.5f;
    [SerializeField] float dashCooldown = 3f;
    public float DashCoolDownTime = 0f;
    public float TeleForce = 5f;
    [Header("Active Skill")]
    [SerializeField] GameObject skillprefab;
    [SerializeField] string activeAnimation;
    [SerializeField] float skillDuration;
    [SerializeField] float skillCoolDown;
    public bool activeCoolDown = false;

    public static bool isCoolDownDash;
    public static bool isCoolDownSkill;
    private void Start()
    {
        checkCollider = GetComponentInChildren<CheckCollider>();
        anim = GetComponent<Animator>();
        playerMeleeAttack = GetComponent<PlayerMeleeAttack>();
        playerController = GetComponent<ThirdPersonController>();
        skillprefab = PoolManager.Instance.SpawnFromPool(GameManager.Instance.currentSkill, skillPosition.position, Quaternion.identity);
        skillprefab.SetActive(false);
        activeAnimation = skillprefab.GetComponent<ActiveSkill>().animationName;
        skillDuration = skillprefab.GetComponent<ActiveSkill>().skillDuration;
        skillCoolDown = skillprefab.GetComponent<ActiveSkill>().skillCoolDown;
        isCoolDownSkill = false;
        isCoolDownDash = false;
    }
    private void Update()
    {
        if(InGameManager.Instance.playerCurrentHealth <= 0)
        {
            return;
        }
        if(Input.GetKeyDown(KeyCode.Alpha1) && !activeCoolDown)
        {
            anim.Play(activeAnimation);
            if (GameManager.Instance.BuffSkill)
            {
                skillprefab.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
                skillprefab.transform.position = new Vector3(skillPosition.position.x, skillPosition.position.y - 0.1f, skillPosition.position.z);
            }
            else
            {
                skillprefab.transform.position = skillPosition.position;
                skillprefab.transform.rotation = skillPosition.rotation;
            }
            StartCoroutine(SkillCoolDown());
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && DashCoolDownTime <= Time.time)
        {
            StartCoroutine(Dash());
        }
        if (DashCoolDownTime > Time.time)
        {
            isCoolDownDash = true;
        }
        else
        {
            isCoolDownDash = false;
        }
    }
    IEnumerator Dash()
    {
        while (DashCoolDownTime <= Time.time)
        {
            float dashForce = TeleForce;
            dashEffect.gameObject.SetActive(true);
            if(checkCollider.isDashAble)
            {
                dashForce = 0f;
            }
            Vector3 telePos = transform.position + transform.forward * dashForce;
            transform.position = telePos;
            DashCoolDownTime = Time.time + dashCooldown;
            playerController.SprintSpeed = dashSpeed;
            anim.speed = 0f;
            yield return new WaitForSeconds(dashTime);
            playerController.SprintSpeed = 9f;
            anim.speed = 1f;
        }
        dashEffect.gameObject.SetActive(false);
    }
    IEnumerator SkillCoolDown()
    {
        SoundManager.Instance.PlayerCharacterBurst(InGameManager.Instance.characterName + "Skill");
        skillprefab.gameObject.SetActive(true);
        activeCoolDown = true;
        isCoolDownSkill = true;
        yield return new WaitForSeconds(skillDuration);
        skillprefab.gameObject.SetActive(false);
        yield return new WaitForSeconds(skillCoolDown);
        activeCoolDown = false;
        isCoolDownSkill = false;
    }
}
