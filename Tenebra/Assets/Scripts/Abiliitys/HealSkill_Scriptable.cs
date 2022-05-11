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
        base.DownClick(abiliityButton);
        GameObject effect = Instantiate(prefabEffect, abiliityButton.GameController.Player.transform);
        int valueTemp = SkillCalculator.CalculeAbility(healValue, abiliityButton.GameController.PlayerStats.MagicSkill.CurrentLevel);
        abiliityButton.GameController.PlayerStats.Heal(valueTemp);
    }
}