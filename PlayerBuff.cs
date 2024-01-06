using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuff : MonoBehaviour
{
    private ActiveSkill activeSkill;

    private void Start()
    {
        activeSkill = GetComponent<ActiveSkill>();
        InGameManager.Instance.playerATK += activeSkill.skillDamage;
    }
}
