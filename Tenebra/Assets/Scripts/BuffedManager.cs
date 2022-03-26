using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffedManager : MonoBehaviour
{
    private float valueTemp;
    public PlayerMoviment playerMoviment;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
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
                valueTemp = playerMoviment.MyDamage;
                playerMoviment.MyDamage = value;
                break;
            case BuffedType.AttackSpeed:
                valueTemp = playerMoviment.MyAttackSpeed;
                playerMoviment.MyAttackSpeed = value;

                break;
            case BuffedType.Armor:
                valueTemp = playerMoviment.MyArmor;
                playerMoviment.MyArmor = value;

                break;
            case BuffedType.Resistence:
                valueTemp = playerMoviment.MyResistence;
                playerMoviment.MyResistence = value;

                break;
            case BuffedType.Life:
                valueTemp = playerMoviment.MyLife;
                playerMoviment.MyLife = value;
                break;
            case BuffedType.MoveSpeed:
                valueTemp = playerMoviment.MyMoveSpeed;
                playerMoviment.MyMoveSpeed = value;
                break;
            default:
                break;
        }

    }
}
