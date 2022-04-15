using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Create AreaSkill", menuName = "Skill/New Heal Skill")]
public class HealSkill_Scriptable : Skills_Scriptable
{
    public GameObject prefabEffect;
    public int healValue;
    public override void DownClick(AbiliityButton abiliityButton)
    {
        GameObject effect = Instantiate(prefabEffect, abiliityButton.gameController.player.transform);
        int valueTemp = SkillCalculator.Calcule(healValue, abiliityButton.gameController.PlayerStats.MagicSkill.CurrentLevel);
        abiliityButton.gameController.PlayerStats.Life.Gain(valueTemp);
    }
}