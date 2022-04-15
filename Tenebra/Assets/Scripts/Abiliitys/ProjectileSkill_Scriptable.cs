using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Create ProjectileSkill", menuName = "Skill/New Projectile Skill")]
public class ProjectileSkill_Scriptable : Skills_Scriptable
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
    public ProjectileType projectileType;
    public int damageBurning;
    public int burningDuration;
    public int moveSpeed;
    public float width;
    public float height;

    private bool isActive;
    private Joystick joy;
    public override void DownClick(AbiliityButton abiliityButton)
    {
        base.DownClick(abiliityButton);
        joy = abiliityButton.Joy;
        abiliityButton.projectileEffect.sprite = imageEffect;
        abiliityButton.projectileEffect.rectTransform.sizeDelta = new Vector2(width, height);
        abiliityButton.gameObject.GetComponent<Image>().enabled = false;
        joy.gameObject.SetActive(true);

    }
    public override void ProjectileRotation(AbiliityButton abiliityButton)
    {
        base.ProjectileRotation(abiliityButton);
        Vector3 direction = new (joy.Horizontal, 0f, joy.Vertical);
        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
            abiliityButton.projectileEffect.rectTransform.rotation = Quaternion.Euler(90, 0, targetAngle);
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            abiliityButton.lookObj.rotation = Quaternion.Euler(0, targetAngle, 0);
            isActive = true;
        }
    }
    public override void UpClick(AbiliityButton abiliityButton)
    {
        base.UpClick(abiliityButton);
        abiliityButton.projectileEffect.gameObject.SetActive(false);
        abiliityButton.gameObject.GetComponent<Image>().enabled = true;
        joy.gameObject.SetActive(false);
        if (isActive)
        {
            abiliityButton.gameController.PlayerController.SkillAnimation(abiliityButton.lookObj, 0.5f, triggerName);
            Cast(abiliityButton);
        }
        isActive = false;
    }
    private void Cast(AbiliityButton abiliityButton)
    {
        Cast c = cast.GetComponent<Cast>();
        c.Constructor(abiliityButton.spanwPoint.transform, prefabEffect, moveSpeed);
        if (animationClip.events.Length <= 0)
        {
            AnimationEvent AnimEvent = new ();
            AnimEvent.functionName = "CastSkill";
            AnimEvent.time = TimeAnimationCast;
            AnimEvent.objectReferenceParameter = c;
            animationClip.AddEvent(AnimEvent);

            AnimationEvent AnimEvent2 = new ();
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