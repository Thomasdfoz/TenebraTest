using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] ProjectileSkill_Scriptable projectileScriptable;
    [SerializeField] DamageType damageType;
    [SerializeField] bool isCritic;
    int damage;
    // Start is called before the first frame update
    void Start()
    {
        damage = projectileScriptable.damage;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Object"))
        {
            SendDamage sendDamage = new SendDamage(damage, isCritic, damageType);
            other.SendMessage("TookDamage", sendDamage, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }
    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
