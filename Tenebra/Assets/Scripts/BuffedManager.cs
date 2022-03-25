using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffedManager : MonoBehaviour
{
    private float valueTemp;
    public PlayerStats playerStats;
    public void Buff(float time, float value, BuffedType buffedType)
    {
        BuffedOrNerfed(value, buffedType);
        object[] parms = new object[3] { time, value, buffedType};
        StartCoroutine("BuffedTime", parms);
    }
    private IEnumerator BuffedTime(object[] parms)
    {
        float time = (float)parms[0];
        float value = (float)parms[1];
        BuffedType buffedType = (BuffedType)parms[2];

        yield return new WaitForSeconds(time);

        BuffedOrNerfed((value * -1), buffedType);
    }
    private void BuffedOrNerfed(float value, BuffedType buffedType)
    {
        switch (buffedType)
        {
            case BuffedType.Damage:
                valueTemp = playerStats.Damage;
                playerStats.Damage = value;
                break;
            case BuffedType.AttackSpeed:
                valueTemp = playerStats.AttackSpeed;
                playerStats.AttackSpeed = value;

                break;
            case BuffedType.Armor:
                valueTemp = playerStats.Defense;
                playerStats.Defense= value;

                break;
            case BuffedType.Resistence:
                valueTemp = playerStats.Resistence;
                playerStats.Resistence = value;

                break;
            case BuffedType.Life:
                valueTemp = playerStats.Life.CurrentValue;
                playerStats.Life.CurrentValue = Mathf.FloorToInt(value);
                break;
            case BuffedType.MoveSpeed:
                valueTemp = playerStats.MoveSpeed;
                playerStats.MoveSpeed = value;
                break;
            default:
                break;
        }

    }
}
