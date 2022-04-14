using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Create AreaSkill", menuName = "Skill/New Area Skill")]
public class AreaSkills_Scriptable : Skills_Scriptable
{
    public GameObject prefabEffect;
    public Sprite imageEffect;
    public int damage;
    public bool isBurning;
    public int damageBurning;
    public int timeDuration;
    public int width;
    public int height;


    private float posY;
    private float posX;
    private float posZ;

    private Joystick joy;
    private bool isMoving;


    public void DownClick(Image areaEffect, GameObject obj, Joystick jo)
    {
        posY = 0;
        posX = 0;
        posZ = 0;
        joy = jo;
        areaEffect.sprite = imageEffect;
        areaEffect.rectTransform.sizeDelta = new Vector2(width, height);
        obj.GetComponent<Image>().enabled = false;
        joy.gameObject.SetActive(true);
        isMoving = true;
        Debug.Log("deu");
    }
    public void UpClick(Image areaEffect, GameObject obj)
    {
        areaEffect.gameObject.SetActive(false);
        obj.GetComponent<Image>().enabled = true;
        joy.gameObject.SetActive(false);
        GameObject magic = Instantiate(prefabEffect);
        Vector3 pos = new Vector3(areaEffect.rectTransform.position.x, prefabEffect.transform.position.y, areaEffect.rectTransform.position.z);
        magic.transform.position = pos;
        isMoving = false;
        posY = 0;
        posX = 0;
        posZ = 0;
        Debug.Log("deu2");
    }
    public void MoveAreaSkill(RectTransform[] limites, Image areaEffect, GameController gameController)
    {
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
            areaEffect.rectTransform.anchoredPosition3D = new Vector3(posX, posY, posZ);
        }
        Vector3 difference = areaEffect.transform.position - gameController.player.transform.position;
        float rotationZ = Mathf.Atan2(difference.z, difference.x) * Mathf.Rad2Deg;
        areaEffect.transform.rotation = Quaternion.Euler(90.0f, 0.0f, rotationZ - 90f);
    }
}

