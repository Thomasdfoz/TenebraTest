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
    public int buttonNumber;
    [Header("Effects")]
    public Image areaEffect;
    public Image projectileEffect;
    public Transform lookObj;
    public RawImage mira;
    [Header("GameObjs")]
    public GameObject spanwPoint;
    public GameObject cicleRanged;
    [Header("Limites")]
    public RectTransform[] limites;
    public Joystick joy;




    #region privates
    //skills types
    Skills_Scriptable skills_Scriptable;

    private bool isAreaSkill;
    private bool isAutoAttackSkill;
    private bool isTargetSkill;
    private bool isProjectileSkill;
    private bool isHealSkill;

    public Joystick Joy { get => joy; set => joy = value; }
    public bool IsAreaSkill { get => isAreaSkill; set => isAreaSkill = value; }
    public bool IsAutoAttackSkill { get => isAutoAttackSkill; set => isAutoAttackSkill = value; }
    public bool IsTargetSkill { get => isTargetSkill; set => isTargetSkill = value; }
    public bool IsProjectileSkill { get => isProjectileSkill; set => isProjectileSkill = value; }
    public bool IsHealSkill { get => isHealSkill; set => isHealSkill = value; }

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        Joy = GetComponentInChildren<FixedJoystick>();
        Joy.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsAreaSkill)
        {
            skills_Scriptable.MoveAreaSkill(this);
        }
        else if (IsProjectileSkill)
        {
            skills_Scriptable.ProjectileRotation(this);
        }

    }
    public void OnPointerUp(PointerEventData data)
    {
        skills_Scriptable.UpClick(this);
        Joy.OnPointerUp(data);
        AllBooleanFalse();
    }
    public void OnPointerDown(PointerEventData data)
    {
        AllBooleanFalse();
        skills_Scriptable = gameController.skill[buttonNumber];
        skills_Scriptable.DownClick(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (joy.gameObject.activeInHierarchy)
        {
            Joy.OnDrag(eventData);
            if (Joy.Vertical > 0.3f || Joy.Horizontal > 0.3f || Joy.Vertical < -0.3f || Joy.Horizontal < -0.3f)
            {
                if (IsAreaSkill)
                {
                    areaEffect.gameObject.SetActive(true);
                }
                else if (IsProjectileSkill)
                {
                    projectileEffect.gameObject.SetActive(true);
                }
                else if (IsAutoAttackSkill)
                {
                    mira.gameObject.SetActive(true);
                }
            }
        }
    }
    private void AllBooleanFalse()
    {
        IsAreaSkill = false;
        IsAutoAttackSkill = false;
        IsTargetSkill = false;
        IsHealSkill = false;
        IsProjectileSkill = false;
    }
}
