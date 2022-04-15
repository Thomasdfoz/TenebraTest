using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Create AreaSkill", menuName = "Skill/New Heal Skill")]
public class HealSkill_Scriptable : Skills_Scriptable
{
    public GameObject prefabEffect;
    public int healValue;
    public override void DownClick(GameController gameController)
    {
        GameObject effect = Instantiate(prefabEffect, gameController.player.transform);
        int valueTemp = SkillCalculator.Calcule(healValue, gameController.PlayerStats.MagicSkill.CurrentLevel);
        gameController.PlayerStats.Life.Gain(valueTemp);
    }
}