using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class AbiliityButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    [Header("Main")]
    public GameController gameController;
    [Header("Effects")]
    public Image areaEffect;
    public Image projectileEffect;
    public RawImage mira;
    [Header("GameObjs")]
    public GameObject spanwPoint;
    public GameObject cicleRanged;
    [Header("Limites")]
    public RectTransform[] limites;




    #region privates
    //skills types
    private AreaSkills_Scriptable areaSkill;
    private ProjectileSkill_Scriptable projectileSkill;
    private AutoAttackSkill_Scriptable autoAttackSkill;
    private Skills_Scriptable autoTargetSkill;
    private HealSkill_Scriptable healSkill;

    private bool isAreaSkill;
    private bool isAutoAttackSkill;
    private bool isAutoTargetSkill;
    private bool isProjectileSkill;
    private bool isHealSkill;

    private Joystick joy;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        joy = GetComponentInChildren<FixedJoystick>();
        joy.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isAreaSkill)
        {
            areaSkill.MoveAreaSkill(limites, areaEffect, gameController);
        }
        else if (isProjectileSkill)
        {
            projectileSkill.ProjectileRotation(projectileEffect, spanwPoint.transform);

        }
    }
    public void OnPointerUp(PointerEventData data)
    {
        if (isAreaSkill)
        {
            areaSkill.UpClick(areaEffect, this.gameObject);
        }
        else if (isProjectileSkill)
        {
            projectileSkill.UpClick(projectileEffect, this.gameObject, spanwPoint.transform);
        }
        else if (isAutoAttackSkill)
        {
            autoAttackSkill.UpClick();
        }

        joy.OnPointerUp(data);
        AllBooleanFalse();
    }
    public void OnPointerDown(PointerEventData data)
    {
        AllBooleanFalse();
        switch (gameController.skill.skillType)
        {
            case SkillType.Area:
                isAreaSkill = true;
                areaSkill = (AreaSkills_Scriptable)gameController.skill;
                areaSkill.DownClick(areaEffect, this.gameObject, joy);
                break;
            case SkillType.Projectile:
                isProjectileSkill = true;
                projectileSkill = (ProjectileSkill_Scriptable)gameController.skill;
                projectileSkill.DownClick(projectileEffect, this.gameObject, joy);
                break;
            case SkillType.AutoAttack:
                isAutoAttackSkill = true;
                autoAttackSkill = (AutoAttackSkill_Scriptable)gameController.skill;
                autoAttackSkill.DownClick(this.gameObject, joy, gameController, cicleRanged, mira);
                break;
            case SkillType.AutoTarget:
                isAutoTargetSkill = true;
                autoTargetSkill = gameController.skill;
                //autoTargetSkill.DownClick();
                break;
            case SkillType.Heal:
                isHealSkill = true;
                healSkill = (HealSkill_Scriptable)gameController.skill;
                //healSkill.DownClick();
                break;
            default:
                break;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        joy.OnDrag(eventData);
        if (joy.Vertical > 0.3f || joy.Horizontal > 0.3f || joy.Vertical < -0.3f || joy.Horizontal < -0.3f)
        {
            if (isAreaSkill)
            {
                areaEffect.gameObject.SetActive(true);
                Debug.Log("entrou aki2");

            }
            else if (isProjectileSkill)
            {
                projectileEffect.gameObject.SetActive(true);
            }
            else if (isAutoAttackSkill)
            {
                mira.gameObject.SetActive(true);
            }
        }
    }
    private void AllBooleanFalse()
    {
        isAreaSkill = false;
        isAutoAttackSkill = false;
        isAutoTargetSkill = false;
        isHealSkill = false;
        isProjectileSkill = false;

    }
    private void HealSkill()
    {
        //skillPrefab = healSkillObject.prefabEffect;
        //GameObject effect = Instantiate(skillPrefab, gameController.player.transform);
        if (healSkill.healValue > 0)
        {
            //tenho que fazaer alguma funcao dentro dos skill pra calcular esse valor para bufar as magias
            //fazer igual o burning damage para dar um burning heal tambem
            Debug.Log("Lembrete no abiliityButton");
            float valueTemp = healSkill.healValue * gameController.PlayerStats.MagicSkill.CurrentLevel;
            gameController.PlayerStats.Life.Gain(healSkill.healValue);
        }
    }
}
