using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] int currentHealth;
    public GameObject damagePopupPrefab;
    private Animator anim;
    private bool isHit;
    private void Start()
    {
        maxHealth = InGameManager.Instance.playerTotalHealth;
        currentHealth = maxHealth;
        InGameManager.Instance.playerCurrentHealth = currentHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int amount)
    {
        anim.Play("Hits");
        isHit = true;
        int reduceDamage = amount - InGameManager.Instance.playerDEF * 15 / 100;
        if(reduceDamage < 0)
            reduceDamage = 0;
        ShowDamageText(reduceDamage);
        InGameManager.Instance.UpdateHealth(reduceDamage);
        currentHealth -= reduceDamage;
        if(currentHealth <= 0 )
        {
            PlayerDie();
        }
        StartCoroutine(HitCoolDown());
    }
    public void ShowDamageText(int damage)
    {
        var go = Instantiate(damagePopupPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = damage.ToString();
    }
    public void PlayerDie()
    {
        anim.Play("Die");
    }
    public void PlayerDestroy()
    {
        this.gameObject.SetActive(false);
    }
    public void StartTimeScale()
    {
        Time.timeScale = 0.1f;
    }
    IEnumerator Regenaration()
    {
        while (currentHealth < maxHealth && !isHit)
        {
            yield return new WaitForSeconds(2f);
            int regen = (int)(5 * InGameManager.Instance.playerRegen + 1);
            currentHealth += regen;
            InGameManager.Instance.UpdateHealth(-regen);
            if(currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
                yield break;
            }
            if(isHit)
            {
                yield break;
            }
        }
    }
    IEnumerator HitCoolDown()
    {
        yield return new WaitForSeconds(2f);
        isHit = false;
        if(currentHealth < maxHealth)
        {
            StartCoroutine(Regenaration());
        }
    }
}
