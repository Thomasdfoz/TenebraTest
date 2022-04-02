using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="Create Skill", menuName ="Skill/New Skill")]
public class SkillsScriptable : ScriptableObject
{
    public string nome;
    public int costMana;
    public int costLife;
    public int damage;
    public GameObject prefabEffect;
    public Sprite outline;
}
