using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSkill : MonoBehaviour
{
    public GameController gameController;
    public SkillsScriptable skillObject;
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
            SendDamage sendDamage = new SendDamage(Mathf.FloorToInt(skillObject.damage), 0, DamageType.magic);
            col.SendMessage("TookDamage", sendDamage);
        }
    }

    private IEnumerator DeathTime()
    {
        yield return new WaitForSeconds(skillObject.timeDuration);
        Destroy(this.gameObject);
    }
}
