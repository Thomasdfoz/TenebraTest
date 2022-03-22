using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private int life;
    [SerializeField] private int damage;
    [SerializeField] private float myArmor;
    [SerializeField] private float myResistence;
    [SerializeField] private DamageType damageType;

    public float MyArmor { get => myArmor; set => myArmor += value; }
    public float MyResistence { get => myResistence; set => myResistence += value; }
    public int Life { 
        get => life; 
        set {
            life += value;
            if (life < 0)
            {
                life = 0;
            }
        } 
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Life == 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void TookDamage(SendDamage sendDamage)
    {

        int damageEnemy = sendDamage.Damage;
        DamageType t = sendDamage.DamageType;
        int criticalChance = sendDamage.CriticalChance;
        float damage = 0;
        float defenseTemp = 0;
        float defensed = 0;
        int damageTaken = 0;
        if (t == DamageType.magic)
        {
            damage = damageEnemy;
            defenseTemp = Random.Range(MyResistence * 0.1f, MyResistence);

        }
        else if (t == DamageType.physical)
        {

            if (IsCritic(criticalChance))
            {
                damage = Random.Range(damageEnemy, damageEnemy * 2);
                Debug.Log("Critico");
            }
            else
            {
                damage = Random.Range((damageEnemy * 0.1f), damageEnemy);
            }

            defenseTemp = Random.Range(MyArmor * 0.1f, MyArmor);
        }

        defensed = 1 - (defenseTemp / 500);
        if (defensed < 0.1f) defensed = 0.1f;
        damageTaken = Mathf.FloorToInt(damage * defensed);
        Life = (damageTaken * -1);
        Debug.Log(damageTaken + ", de dano tomado. " + (1 - defensed).ToString("P") + " defendido, dano inimigo " + damage + " defesa ," + defenseTemp );

    }
    public bool IsCritic(int chance)
    {
        if (Random.Range(0, 100) < chance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
