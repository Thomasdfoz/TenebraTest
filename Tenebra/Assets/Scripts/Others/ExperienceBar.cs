using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    public PlayerStats playerStats;
    public Image expFilled;
    public Text expText;

    long currentExp;
    long nextExp;
    long previousExp;
    long maxValue;
    long currentValue;

    public void UpdateExpBar()
    {
        currentExp = playerStats.Level.CurrentExp;
        nextExp = playerStats.Level.NextExpLevel;
        previousExp = playerStats.Level.PreviousExpLevel;
        maxValue = nextExp - previousExp;
        currentValue = currentExp - previousExp;
        expText.text = currentExp.ToString();
        expFilled.fillAmount = (float)currentValue / maxValue;
    }
}
