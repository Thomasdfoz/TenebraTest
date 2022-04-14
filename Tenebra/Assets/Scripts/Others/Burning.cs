using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burning : MonoBehaviour
{
    const int BURNINGTIME = 2;

    public string source;
    public float timeBurning;
    public float time;
    public int damage;
    public DamageType damageType;
    public bool isReady;

    
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        StartCoroutine("IsBurningReady");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        
        if (time >= timeBurning + 0.2f)
        {
            Destroy(this);
        }
        if (isReady)
        {
            BurningDamage();
            StartCoroutine("IsBurningReady");
        }
    }
    public void Constructor(int damage, float timeBurning, DamageType damageType, string source)
    {
        this.damage = damage;
        this.timeBurning = timeBurning;
        this.damageType = damageType;
        this.source = source;
    }
    public void ResetTime(int damage)
    {
        time = 0;
        this.damage = damage;
    }

    private IEnumerator IsBurningReady()
    {
        isReady = false;
        yield return new WaitForSeconds(BURNINGTIME);
        isReady = true;
    } 
    private void BurningDamage()
    {
        SendDamage sendDamage = new SendDamage(damage, false, damageType);
        this.SendMessage("TookDamage", sendDamage, SendMessageOptions.DontRequireReceiver);
    }

}
