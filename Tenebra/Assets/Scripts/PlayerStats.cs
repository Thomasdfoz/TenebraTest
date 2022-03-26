using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region ------------------------Main--------------------------
    WaeponType waeponType;
    public BuffedManager buffedManager;
    #endregion

    #region ------------------------Stats--------------------------
    private AttributesClass life = new AttributesClass(100);
    private AttributesClass mana = new AttributesClass(10);
    private LevelClass level = new LevelClass();
    private float damage;
    private float defense;
    private float resistence;
    private float moveSpeed;
    private float attackSpeed;
    private float range;
    #endregion

    #region ------------------------Skills--------------------------
    private SkillClass meleeSkill = new SkillClass();
    private SkillClass distanceSkill = new SkillClass();
    private SkillClass magicSkill = new SkillClass();
    private SkillClass defenseSkill = new SkillClass();

    public float Damage
    {
        get
        {
            if (waeponType == WaeponType.melee)
            {
                return (damage * (MeleeSkill.CurrentLevel / 100)) + damage;
            }
            else if (waeponType == WaeponType.distance)
            {
                return ((DistanceSkill.CurrentLevel / 100) * damage) + damage;
            }
            else if (waeponType == WaeponType.magic)
            {
                return ((MagicSkill.CurrentLevel / 100) * damage) + damage;
            }
            else
            {
                return ((MeleeSkill.CurrentLevel / 100) * 2) + 5;
            }
        }

        set
        {
            damage += value;
            if (damage < 1)
            {
                damage = 1;
            }
        }
    }
    public float Defense
    {
        get => ((DefenseSkill.CurrentLevel / 100) * defense) + defense;
        set
        {
            defense += value;
            if (defense < 0)
            {
                defense = 0;
            }
        }
    }
    public float Resistence
    {
        get => resistence;
        set
        {
            resistence += value;
            if (resistence < 0)
            {
                resistence = 0;
            }
        }
    }
    public float MoveSpeed
    {
        get => moveSpeed;
        set
        {
            moveSpeed += value;
            if (moveSpeed < 1)
            {
                moveSpeed = 1;
            }
        }
    }
    public float AttackSpeed
    {
        get => (1 / (attackSpeed / 100));
        set
        {
            if (value > 200)
            {
                attackSpeed = 200;
            }
            else if (value < 25)
            {
                attackSpeed = 25;
            }
            else
            {
                attackSpeed = value;
            }

        }
    }
    public float Range
    {
        get => range;
        set
        {
            if (range > 8)
            {
                range = 8;
            }
            else if (range < 1.5f)
            {
                range = 1.5f;
            }
            else
            {
                range = value;
            }
        }
    }
    public SkillClass MeleeSkill { get => meleeSkill; set => meleeSkill = value; }
    public SkillClass DistanceSkill { get => distanceSkill; set => distanceSkill = value; }
    public SkillClass MagicSkill { get => magicSkill; set => magicSkill = value; }
    public SkillClass DefenseSkill { get => defenseSkill; set => defenseSkill = value; }
    public AttributesClass Life { get => life; set => life = value; }
    public AttributesClass Mana { get => mana; set => mana = value; }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        MoveSpeed = 1;
        Range = 1;
        Damage = 1;
        AttackSpeed = 1;
        Defense = 0;
        Resistence = 0;

    }

    // Update is called once per frame
    void Update()
    {

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
            defenseTemp = Random.Range(Resistence * 0.1f, Resistence);

        }
        else if (t == DamageType.physical)
        {

            if (Critic.IsCritic(criticalChance))
            {
                damage = Random.Range(damageEnemy, damageEnemy * 2);
                Debug.Log("Critico");
            }
            else
            {
                damage = Random.Range((damageEnemy * 0.1f), damageEnemy);
            }

            defenseTemp = Random.Range(Defense * 0.1f, Defense);
        }

        defensed = 1 - (defenseTemp / 500);
        if (defensed < 0.1f) defensed = 0.1f;
        damageTaken = Mathf.FloorToInt(damage * defensed);
        Debug.Log(damageTaken + ", de dano tomado. " + (1 - defensed) * 100 + "% defendido, dano inimigo " + damage + " defesatemp ," + defenseTemp + " My," + Damage);
    }
    public void Buff()
    {
        buffedManager.Buff(2, 100, BuffedType.Armor, this);
    }


}
