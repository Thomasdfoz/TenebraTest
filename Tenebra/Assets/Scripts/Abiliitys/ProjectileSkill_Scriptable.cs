using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Create ProjectileSkill", menuName = "Skill/New Projectile Skill")]
public class ProjectileSkill_Scriptable : Skills_Scriptable
{
    public GameObject prefabEffect;
    public Sprite imageEffect;
    public bool isBurning;
    public ProjectileType projectileType;
    public int damage;
    public int damageBurning;
    public int burningDuration;
    public int moveSpeed;
    public int width;
    public int height;


    private Joystick joy;

    public void DownClick(Image projectileEffect, GameObject obj, Joystick jo)
    {
        joy = jo;
        projectileEffect.sprite = imageEffect;
        projectileEffect.rectTransform.sizeDelta = new Vector2(width, height);
        obj.GetComponent<Image>().enabled = false;
        joy.gameObject.SetActive(true);
    }
    public void ProjectileRotation(Image projectileEffect, Transform spanwPoint)
    {
        Vector3 direction = new Vector3(joy.Horizontal, 0f, joy.Vertical).normalized;
        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
            projectileEffect.rectTransform.rotation = Quaternion.Euler(90, 0, targetAngle);
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            spanwPoint.rotation = Quaternion.Euler(0, targetAngle, 0);
        }
    }
    public void  UpClick(Image projectileEffect, GameObject obj, Transform spanwPoint)
    {
        projectileEffect.gameObject.SetActive(false);
        obj.GetComponent<Image>().enabled = true;
        joy.gameObject.SetActive(false);
        GameObject missile = Instantiate(prefabEffect, spanwPoint.transform.position, spanwPoint.transform.rotation);
        missile.GetComponent<Rigidbody>().velocity = (spanwPoint.transform.forward * moveSpeed);
    }
}