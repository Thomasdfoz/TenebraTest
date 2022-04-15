using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Create AreaSkill", menuName = "Skill/New Area Skill")]
public class AreaSkills_Scriptable : Skills_Scriptable
{
    public int damage;
    public GameObject prefabEffect;
    public Sprite imageEffect;
    public AnimationClip animationClip;
    public float TimeAnimationCast;
    public float TimeAnimation;
    public string triggerName;
    public GameObject cast;
    public bool isBurning;
    public int damageBurning;
    public int timeDuration;
    public float width;
    public float height;


    private float posY;
    private float posX;
    private float posZ;

    private bool isActive;
    private Joystick joy;
    private bool isMoving;


    public override void DownClick(AbiliityButton abiliityButton)
    {
        base.DownClick(abiliityButton);
        posY = 0;
        posX = 0;
        posZ = 0;
        joy = abiliityButton.Joy;
        abiliityButton.areaEffect.sprite = imageEffect;
        abiliityButton.areaEffect.rectTransform.sizeDelta = new Vector2(width, height);
        abiliityButton.gameObject.GetComponent<Image>().enabled = false;
        joy.gameObject.SetActive(true);
        isMoving = true;
    }
    public override void UpClick(AbiliityButton abiliityButton)
    {
        base.UpClick(abiliityButton);
        abiliityButton.areaEffect.gameObject.SetActive(false);
        abiliityButton.gameObject.GetComponent<Image>().enabled = true;
        joy.gameObject.SetActive(false);
        if (isActive)
        {
            abiliityButton.gameController.PlayerController.SkillAnimation(abiliityButton.areaEffect.transform, 0.5f, triggerName);
        }
        else
        {
            abiliityButton.gameController.PlayerController.SkillAnimation(0.5f, triggerName);
        }
        Cast(abiliityButton);
        isMoving = false;
        isActive = false;
        posY = 0;
        posX = 0;
        posZ = 0;
    }
    public override void MoveAreaSkill(AbiliityButton abiliityButton)
    {
        base.MoveAreaSkill(abiliityButton);
        RectTransform[] limites = abiliityButton.limites;
        if (isMoving)
        {
            posX += (joy.Horizontal / 4);
            posZ += (joy.Vertical / 4);

            if (posX > limites[3].anchoredPosition3D.x)
            {
                posX = limites[3].anchoredPosition3D.x;
            }
            if (posX < limites[1].anchoredPosition3D.x)
            {
                posX = limites[1].anchoredPosition3D.x;
            }
            if (posZ > limites[2].anchoredPosition3D.z)
            {
                posZ = limites[2].anchoredPosition3D.z;
            }
            if (posZ < limites[0].anchoredPosition3D.z)
            {
                posZ = limites[0].anchoredPosition3D.z;

            }
            abiliityButton.areaEffect.rectTransform.anchoredPosition3D = new Vector3(posX, posY, posZ);
            if (abiliityButton.areaEffect.rectTransform.anchoredPosition3D.magnitude > 0.1f)
            {
                isActive = true;
            }
        }
        Vector3 difference = abiliityButton.areaEffect.transform.position - abiliityButton.gameController.player.transform.position;
        float rotationZ = Mathf.Atan2(difference.z, difference.x) * Mathf.Rad2Deg;
        abiliityButton.areaEffect.transform.rotation = Quaternion.Euler(90.0f, 0.0f, rotationZ - 90f);
    }
    private void Cast(AbiliityButton abiliityButton)
    {
        Cast c = cast.GetComponent<Cast>();
        c.Constructor(abiliityButton.areaEffect.transform, prefabEffect, 0);
        if (animationClip.events.Length <= 0)
        {
            AnimationEvent AnimEvent = new();
            AnimEvent.functionName = "CastSkill";
            AnimEvent.time = TimeAnimationCast;
            AnimEvent.objectReferenceParameter = c;
            animationClip.AddEvent(AnimEvent);

            AnimationEvent AnimEvent2 = new();
            AnimEvent2.functionName = "AnimationEnd";
            AnimEvent2.time = TimeAnimation;
            animationClip.AddEvent(AnimEvent2);
        }
        else
        {
            animationClip.events[0].objectReferenceParameter = c;
        }
        abiliityButton.gameController.PlayerController.IsAnimationEnd = false;
    }
}


