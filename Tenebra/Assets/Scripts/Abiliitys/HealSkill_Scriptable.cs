using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Create AreaSkill", menuName = "Skill/New Heal Skill")]
public class HealSkill_Scriptable : Skills_Scriptable
{
    public GameObject prefabEffect;
    public float healValue;
}