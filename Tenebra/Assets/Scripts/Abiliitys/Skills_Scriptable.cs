using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class Skills_Scriptable :  ScriptableObject
{
    public SkillType skillType;
    public Sprite iconImage;
    public string nameSkill;
    public int costMana;
    public float countdown;


    public virtual void DownClick(){}
    public virtual void DownClick(GameObject obj, Joystick joy, GameController gameController, GameObject cicleRanged, RawImage mira){}
    public virtual void DownClick(GameController gameController){}
    public virtual void DownClick(Image projectileEffect, GameObject obj, Joystick jo){}
    public virtual void UpClick(){}
    public virtual void UpClick(Image areaEffect, GameObject obj){}
    public virtual void UpClick(Image projectileEffect, GameObject obj, Transform spanwPoint){}

    #region Outhers methods
    public virtual void ProjectileRotation(Image projectileEffect, Transform spanwPoint){}
    public virtual void MoveAreaSkill(RectTransform[] limites, Image areaEffect, GameController gameController){}
        #endregion


        /*
        public int costLife;
        public int damage;
        public int damageBurning;
        public int timeDuration;
        public GameObject prefabEffect;
        public Sprite imageEffect;
        public int width;
        public int height;
        */
    }
