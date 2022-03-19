using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendDamage
{
    private int damage;
    private int criticalChance;
    private DamageType damageType;

    public SendDamage(int damage, int criticalChance, DamageType damageType)
    {
        this.damage = damage;
        this.criticalChance = criticalChance;
        this.damageType = damageType;
    }

    public int Damage { get => damage; set => damage = value; }
    public int CriticalChance { get => criticalChance; set => criticalChance = value; }
    public DamageType DamageType { get => damageType; set => damageType = value; }

}
