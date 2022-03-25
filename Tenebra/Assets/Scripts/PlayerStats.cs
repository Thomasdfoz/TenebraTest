using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
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
    #endregion

    #region ------------------------Main--------------------------
    public GameObject player;
    public GameObject playerBody;
    public GameObject canvasPlayer;
    public PlayerMoviment playerMoviment;
    public GameController gameController;
    private WaeponType waeponType;
    #endregion


    #region getters and Setters
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
    public float Defense
    {
        get => ((DefenseSkill.CurrentLevel / 100) * defense) + defense;
        set
        {
            defense += value;
            if (defense < 10)
            {
                defense = 10;
            }
        }
    }
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
    public float Resistence
    {
        get => resistence;
        set
        {
            resistence += value;
            if (resistence < 10)
            {
                resistence = 10;
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

    public AttributesClass Life { get => life; set => life = value; }
    public AttributesClass Mana { get => mana; set => mana = value; }
    public LevelClass Level { get => level; set => level = value; }
    public SkillClass MeleeSkill { get => meleeSkill; set => meleeSkill = value; }
    public SkillClass DistanceSkill { get => distanceSkill; set => distanceSkill = value; }
    public SkillClass MagicSkill { get => magicSkill; set => magicSkill = value; }
    public SkillClass DefenseSkill { get => defenseSkill; set => defenseSkill = value; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        playerMoviment = GetComponentInChildren<PlayerMoviment>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Life.CurrentValue <= 0)
        {
            IsDead();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Life.CurrentValue = -10;
        }
        Debug.Log(Life.CurrentValue);
        
    }
    public void IsDead()
    {
        playerBody.SetActive(false);
        canvasPlayer.SetActive(false);
        Level.CurrentExp = ((Level.CurrentExp * 0.08f) * -1);
        MeleeSkill.CurrentExp = ((MeleeSkill.CurrentExp * 0.08f) * -1);
        DistanceSkill.CurrentExp = ((DistanceSkill.CurrentExp * 0.08f) * -1);
        MagicSkill.CurrentExp = ((MagicSkill.CurrentExp * 0.08f) * -1);
        DefenseSkill.CurrentExp = ((DefenseSkill.CurrentExp * 0.08f) * -1);
    }
}
