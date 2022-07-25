using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class AbiliityButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    public int buttonNumber;
    public Image countDownFilled;
    public Text countDownText;

    private GameController gameController;
    private PadSkillButton padSkillButton;
    private Joystick joy;

    private bool isAreaSkill;
    private bool isAutoAttackSkill;
    private bool isTargetSkill;
    private bool isProjectileSkill;
    private bool isHealSkill;
    private bool inCountDown;
    private float timeCountDown;

    private Skills_Scriptable skills_Scriptable;

    public bool IsAreaSkill { get => isAreaSkill; set => isAreaSkill = value; }
    public bool IsAutoAttackSkill { get => isAutoAttackSkill; set => isAutoAttackSkill = value; }
    public bool IsTargetSkill { get => isTargetSkill; set => isTargetSkill = value; }
    public bool IsProjectileSkill { get => isProjectileSkill; set => isProjectileSkill = value; }
    public bool IsHealSkill { get => isHealSkill; set => isHealSkill = value; }

    public Joystick Joy { get => joy; set => joy = value; }
    public PadSkillButton PadSkillButton { get => padSkillButton; set => padSkillButton = value; }
    public GameController GameController { get => gameController; set => gameController = value; }

    private void Awake()
    {
        GameController = FindObjectOfType<GameController>();

        padSkillButton = FindObjectOfType<PadSkillButton>();
        Joy = GetComponentInChildren<FixedJoystick>();
    }


    void Start()
    {
        Joy.gameObject.SetActive(false);
        inCountDown = false;
        countDownFilled.enabled = false;
        countDownText.enabled = false;
        ChangeIconImage();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (inCountDown)
        {
            timeCountDown -= Time.deltaTime;
            countDownFilled.fillAmount = timeCountDown / skills_Scriptable.countdown;
            countDownText.text = timeCountDown.ToString("0");
            if (timeCountDown <= 0)
            {
                inCountDown = false;
                countDownFilled.enabled = false;
                countDownText.enabled = false;
            }
        }
        else
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
    }
    public void OnPointerUp(PointerEventData data)
    {
        if (!inCountDown)
        {
            skills_Scriptable.UpClick(this);
            Joy.OnPointerUp(data);
            if (skills_Scriptable.IsSuccess)
            {
                CountDown();
            }
            AllBooleanFalse();
        }
    }
    public void OnPointerDown(PointerEventData data)
    {
        if (!inCountDown)
        {
            AllBooleanFalse();
            skills_Scriptable = GameController.skill[buttonNumber];
            skills_Scriptable.DownClick(this);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!inCountDown)
        {
            if (joy.gameObject.activeInHierarchy)
            {
                Joy.OnDrag(eventData);
                if (Joy.Vertical > 0.3f || Joy.Horizontal > 0.3f || Joy.Vertical < -0.3f || Joy.Horizontal < -0.3f)
                {
                    if (IsAreaSkill)
                    {
                        PadSkillButton.areaEffect.gameObject.SetActive(true);
                    }
                    else if (IsProjectileSkill)
                    {
                        PadSkillButton.projectileEffect.gameObject.SetActive(true);
                    }
                    else if (IsAutoAttackSkill)
                    {
                        PadSkillButton.mira.gameObject.SetActive(true);
                    }
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

    private void CountDown()
    {
        timeCountDown = skills_Scriptable.countdown;
        inCountDown = true;
        countDownFilled.enabled = true;
        countDownText.enabled = true;
    }

    private void ChangeIconImage()
    {
        GetComponent<Image>().sprite = GameController.skill[buttonNumber].iconImage;
    }

}
