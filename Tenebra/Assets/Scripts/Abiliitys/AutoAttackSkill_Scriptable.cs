using System.Collections;
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


    private bool isLook = false;
    private GameController gameController;
    private GameObject cicleRanged;
    private GameObject obj;
    private RawImage mira;
    Joystick joy;

    public void DownClick(GameObject obj, Joystick joy, GameController gameController, GameObject cicleRanged, RawImage mira)
    {
        this.gameController = gameController;
        this.obj = obj;
        this.cicleRanged = cicleRanged;
        this.joy = joy;
        this.mira = mira;

        this.obj.GetComponent<Image>().enabled = false;
        this.joy.gameObject.SetActive(true);
        this.gameController.PlayerController.RangedSelectTarget();
        this.cicleRanged.SetActive(true);
        //essa animaçao vai estar dentro do scriptable tambem "animSpecialAttack"
        if (gameController.PlayerController.SelectedTarget)
        {
            gameController.PlayerController.ReadySpecialAttack();
            gameController.PlayerController.AttackSelected();
            Attack();
        }
        else
        {
            Debug.Log("Nenhum Alvo Selecionado");
        }
    }
    public void UpClick()
    {
        cicleRanged.SetActive(false);
        obj.GetComponent<Image>().enabled = true;
        joy.gameObject.SetActive(false);
        mira.gameObject.SetActive(false);
        mira.rectTransform.localPosition = new Vector3(0, 0, 0);
    }
    public void Attack()
    {
        string parms = damageType.ToString() + "/" + damage.ToString() + "/" + waeponType.ToString();
        if (animationClip.events.Length <= 0)
        {
            AnimationEvent AnimEvent = new AnimationEvent();
            AnimEvent.functionName = "AutoAttackSkill";
            AnimEvent.time = TimeAnimation;
            AnimEvent.stringParameter = parms;
            animationClip.AddEvent(AnimEvent);
        }
        else
        {
            animationClip.events[0].stringParameter = parms;
        }
        gameController.PlayerController.SetTrigger(triggerName, 0.3f);
    }

}