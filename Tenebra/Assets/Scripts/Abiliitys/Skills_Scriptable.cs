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
