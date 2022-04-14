using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AreaSkill : MonoBehaviour
{
    const int BURNINGTIME = 2;

    public AreaSkills_Scriptable skillObject;
    private List<Collider> targets = new List<Collider>();
    private GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>() as GameController;
        StartCoroutine("DeathTime");
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Object"))
        {
            if (!targets.Contains(col))
            {
                targets.Add(col);
                if (skillObject.isBurning)
                {
                    StartCoroutine("BurningDamage", col);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Object"))
        {
            SendDamage sendDamage = new SendDamage(Mathf.FloorToInt(skillObject.damage), false, DamageType.magic);
            col.SendMessage("TookDamage", sendDamage, SendMessageOptions.DontRequireReceiver);
        }

    }
    private IEnumerator DeathTime()
    {
        yield return new WaitForSeconds(skillObject.timeDuration);
        StopAllCoroutines();
        Destroy(this.gameObject);
    }
    private IEnumerator BurningDamage(Collider col)
    {
        yield return new WaitForSeconds(BURNINGTIME);
        if (col != null)
        {
            SendDamage sendDamage = new SendDamage(Mathf.FloorToInt(skillObject.damageBurning), false, DamageType.magic);
            col.SendMessage("TookDamage", sendDamage, SendMessageOptions.DontRequireReceiver);
        }
        if (targets.Contains(col))
        {
            StartCoroutine("BurningDamage", col);
        }
    }
}
