using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffedManager : MonoBehaviour
{
    private float valueTemp;
    public void Buff(float time, float value, BuffedType buffedType, PlayerStats target)
    {
        BuffedOrNerfed(value, buffedType, target);
        object[] parms = new object[4] { time, value, buffedType, target };
        StartCoroutine("BuffedTime", parms);
    }
    private IEnumerator BuffedTime(object[] parms)
    {
        float time = (float)parms[0];
        float value = (float)parms[1];
        BuffedType buffedType = (BuffedType)parms[2];
        PlayerStats target = (PlayerStats)parms[3];

        yield return new WaitForSeconds(time);

        BuffedOrNerfed((value * -1), buffedType, target);
    }
    private void BuffedOrNerfed(float value, BuffedType buffedType, PlayerStats target)
    {
        switch (buffedType)
        {
            case BuffedType.Damage:
                valueTemp = target.Damage;
                target.Damage = value;
                break;
            case BuffedType.AttackSpeed:
                valueTemp = target.AttackSpeed;
                target.AttackSpeed = value;

                break;
            case BuffedType.Armor:
                valueTemp = target.Defense;
                target.Defense = value;

                break;
            case BuffedType.Resistence:
                valueTemp = target.Resistence;
                target.Resistence = value;

                break;
            case BuffedType.Life:
                valueTemp = target.Life.CurrentValue;
                target.Life.Gain(Mathf.FloorToInt(value));
                break;
            case BuffedType.MoveSpeed:
                valueTemp = target.MoveSpeed;
                target.MoveSpeed = value;
                break;
            default:
                break;
        }

    }
}
