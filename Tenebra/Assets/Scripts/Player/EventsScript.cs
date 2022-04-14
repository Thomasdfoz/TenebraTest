using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsScript : MonoBehaviour
{
    public PlayerController playerController;
    private void AutoAttackMelee()
    {
        playerController.AutoAttackMelee();
    }
    private void AnimationEnd()
    {
        playerController.AnimationEnd();
    }
    private void AutoAttackDistance()
    {
        playerController.AutoAttackMelee();
    }
    private void AutoAttackSkill(string parms)
    {
        string[] cut = parms.Split('/');
        string StringDamageType = cut[0];
        float damage = float.Parse(cut[1]);
        string StringwaeponType = cut[2];
        DamageType damageType;

        if (StringDamageType == DamageType.physical.ToString())
        {
            damageType = DamageType.physical;
        }
        else
        {
            damageType = DamageType.magic;
        }
        if (StringwaeponType == WaeponType.melee.ToString() || StringwaeponType == WaeponType.none.ToString())
        {
            playerController.SpecialAttackMelee(damageType, damage);
        }
        else
        {
            playerController.SpecialAttackDistance(damageType, damage);
        }
    }

}
