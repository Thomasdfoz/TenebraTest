using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AreaSkill : MonoBehaviour
{
    const int BURNINGTIME = 2;

    public AreaSkills_Scriptable skillObject;
    private List<Collider> targetsBurningDamage = new List<Collider>();
    private GameController gameController;
    private GameObject target;
    private bool startDamage = false;
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>() as GameController;
        StartCoroutine(DeathTime());
        StartCoroutine(StartDamage());
    }
    

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Object"))
        {
            if (!targetsBurningDamage.Contains(col))
            {
                targetsBurningDamage.Add(col);
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
            if (!startDamage)
            {
                SendDamage sendDamage = new SendDamage(Mathf.FloorToInt(skillObject.damage), false, DamageType.magic);
                col.SendMessage("TookDamage", sendDamage, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Object"))
        {
            if (targetsBurningDamage.Contains(col))
            {
                targetsBurningDamage.Remove(col);
            }
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
        if (targetsBurningDamage.Contains(col))
        {
            StartCoroutine("BurningDamage", col);
        }
    }
    private IEnumerator StartDamage()
    {
        yield return new WaitForSeconds(0.1f);
        startDamage = true;
    }
}

