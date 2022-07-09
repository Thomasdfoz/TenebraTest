using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class Skills_Scriptable : ScriptableObject
{
    [Header("Skills Options")]
    public SkillType skillType;
    public Sprite iconImage;
    public string nameSkill;
    public int costMana;
    public float countdown;
    private bool isSuccess;

    /// <summary>
    /// Usado para pegar o script do cast e das imagens
    /// </summary>
    protected PadSkillButton padSkillButton;

    public bool IsSuccess { get => isSuccess; set => isSuccess = value; }

    public virtual void DownClick(AbiliityButton abiliityButton)
    {
        padSkillButton = abiliityButton.PadSkillButton;
        IsSuccess = true;

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

}
