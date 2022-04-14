using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendDamage
{
    private int damage;
    private bool isCritical;
    private DamageType damageType;

    public SendDamage(int damage, bool isCritical, DamageType damageType)
    {
        this.damage = damage;
        this.isCritical = isCritical;
        this.damageType = damageType;
    }

    public int Damage { get => damage; set => damage = value; }
    public bool IsCritical { get => isCritical; set => isCritical = value; }
    public DamageType DamageType { get => damageType; set => damageType = value; }

}
