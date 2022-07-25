﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Create AutoAttackSkill", menuName = "Skill/New Auto Attack Skill")]
public class AutoAttackSkill_Scriptable : Skills_Scriptable
{
    public AnimationClip animationClip;
    public string triggerName;
    public int damage;
    public DamageType damageType;
    public WaeponType waeponType;
    public float TimeAnimation;
    public float TimeAnimationDamage;


    private bool isLook = false;
    private GameController gameController;
    private GameObject obj;
    Joystick joy;
    public override void DownClick(AbiliityButton abiliityButton)
    {
        
        base.DownClick(abiliityButton);
        this.gameController = abiliityButton.GameController;
        this.obj = abiliityButton.gameObject;
        this.joy = abiliityButton.Joy;
        this.joy.gameObject.SetActive(true);
        this.gameController.PlayerController.RangedSelectTarget();
        padSkillButton.cicleRanged.SetActive(true);
        //essa animaçao vai estar dentro do scriptable tambem "animSpecialAttack"
        if (gameController.PlayerController.SelectedTarget)
        {
            if (gameController.PlayerController.IsAnimationEnd)
            {
                gameController.PlayerController.ReadySpecialAttack();
                gameController.PlayerController.AttackSelected();
                Attack();
            }
            else
            {
                IsSuccess = false;
                Debug.Log("Não pode attacar 2x ao mesmo tempo");
            }
        }
        else
        {
            IsSuccess = false;
            Debug.Log("Nenhum Alvo Selecionado");
        }
    }
   
    public override void UpClick(AbiliityButton abiliityButton)
    {
        padSkillButton.cicleRanged.SetActive(false);
        obj.GetComponent<Image>().enabled = true;
        joy.gameObject.SetActive(false);
        padSkillButton.mira.gameObject.SetActive(false);
        padSkillButton.mira.rectTransform.localPosition = new Vector3(0, 0, 0);
    }
    private void Attack()
    {
        string parms = damageType.ToString() + "/" + damage.ToString() + "/" + waeponType.ToString();
        if (animationClip.events.Length <= 0)
        {
            AnimationEvent AnimEvent = new ();
            AnimEvent.functionName = "AutoAttackSkill";
            AnimEvent.time = TimeAnimationDamage;
            AnimEvent.stringParameter = parms;
            animationClip.AddEvent(AnimEvent);

            AnimationEvent AnimEvent2 = new ();
            AnimEvent2.functionName = "AnimationEnd";
            AnimEvent2.time = TimeAnimation;
            animationClip.AddEvent(AnimEvent2);
        }
        else
        {
            animationClip.events[0].stringParameter = parms;
        }
        gameController.PlayerController.SetTrigger(triggerName, 0.3f);
        gameController.PlayerController.IsAnimationEnd = false;
    }

}