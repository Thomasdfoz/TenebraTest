using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class Skills_Scriptable : ScriptableObject
{
    public SkillType skillType;
    public Sprite iconImage;
    public string nameSkill;
    public int costMana;
    public float countdown;


    public virtual void DownClick(AbiliityButton abiliityButton)
    {
        switch (skillType)
        {
            case SkillType.Area:
                abiliityButton.IsAreaSkill = true;
                break;
            case SkillType.Projectile:
                abiliityButton.IsProjectileSkill = true;
                break;
            case SkillType.AutoAttack:
                abiliityButton.IsAutoAttackSkill = true;
                break;
            case SkillType.Target:
                abiliityButton.IsTargetSkill = true;
                break;
            case SkillType.Heal:
                abiliityButton.IsHealSkill = true;
                break;
            default:
                break;
        }

    }
    public virtual void UpClick(AbiliityButton abiliityButton) { }

    #region Outhers methods
    public virtual void ProjectileRotation(AbiliityButton abiliityButton) { }
    public virtual void MoveAreaSkill(AbiliityButton abiliityButton) { }
    #endregion


    /*
    public int costLife;
    public int damage;
    public int damageBurning;
    public int timeDuration;
    public GameObject prefabEffect;
    public Sprite imageEffect;
    public int width;
    public int height;
    */
}
