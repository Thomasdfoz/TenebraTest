using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private CombatTextManager combatTextManager;
    [SerializeField] private float Speed;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float rangeAttack;
    [SerializeField] private Material materialHit;
    [SerializeField] SkinnedMeshRenderer render;
    [SerializeField] private float maxLife;
    [SerializeField] private int damage;
    [SerializeField] private int chanceCritic;
    [SerializeField] private float myArmor;
    [SerializeField] private float myResistence;
    [SerializeField] private DamageType damageType;
    [SerializeField] private GameObject body;


    [SerializeField] private GameObject target;
    private Animator anim;
    private Collider col;
    private NavMeshAgent agent;
    private Outline outline;
    private float currentLife;

    private bool isReadyAttack;
    private float moveSpeed;
    private Material mat;
    private bool dead;
    private bool isReadyWalk;

    public float MyArmor { get => myArmor; set => myArmor += value; }
    public float MyResistence { get => myResistence; set => myResistence += value; }
    public float CurrentLife
    {
        get => currentLife;
        set
        {
            currentLife += Mathf.FloorToInt(value);
            if (currentLife > MaxLife) currentLife = MaxLife;
            if (currentLife < 0) currentLife = 0;
        }
    }

    public float MaxLife { get => maxLife; set => maxLife = value; }
    public float MoveSpeed { get { return moveSpeed / 1000; } set => moveSpeed = value; }

    public bool Dead { get => dead; set => dead = value; }
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

    private void Awake()
    {
        MoveSpeed = Speed;
        render = GetComponentInChildren<SkinnedMeshRenderer>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider>();
        outline = GetComponent<Outline>();
        agent = GetComponent<NavMeshAgent>();
        mat = render.material;
        CurrentLife = MaxLife;

    }
    void Start()
    {
        isReadyAttack = true;
        isReadyWalk = true;
        agent.speed = MoveSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!dead)
        {

            if (currentLife == 0)
            {
                StartCoroutine(DeadCourotine());
            }
            if (target)
            {
                if (!target.GetComponent<PlayerStats>().IsDead)
                {
                    if (isReadyWalk)
                    {
                        MovePosition(target.transform.position);
                    }
                }
            }
            else
            {
                agent.isStopped = true;
            }
            if (agent.velocity != new Vector3(0, 0, 0))
            {
                anim.SetBool("Run", true);
            }
            else
            {
                anim.SetBool("Run", false);
            }
        }

    }
    public void MovePosition(Vector3 position)
    {
        agent.isStopped = false;
        agent.SetDestination(position);
        if (Vector3.Distance(transform.position, target.transform.position) < rangeAttack)
        {
            AttackAnim();
        }
    }
    public void StartCountDownAttack()
    {
        isReadyWalk = true;
        StartCoroutine(AttackCountDown());
    }
    public void AttackAnim()
    {
        if (isReadyAttack)
        {
            isReadyWalk = false;
            isReadyAttack = false;
            anim.SetTrigger("Attack");
        }
    }
    private void DamageAttack()
    {
        bool isCritic = Critic.IsCritic(chanceCritic);
        SendDamage sendDamage = new SendDamage(damage, isCritic, damageType);
        target.SendMessage("TookDamage", sendDamage, SendMessageOptions.DontRequireReceiver);
    }
    public void TookDamage(SendDamage sendDamage)
    {
        if (!Dead)
        {
            int damageEnemy = sendDamage.Damage;
            DamageType t = sendDamage.DamageType;
            bool isCritical = sendDamage.IsCritical;
            float defenseTemp = 0;
            if (t == DamageType.magic)
            {
                defenseTemp = Random.Range(MyResistence * 0.1f, MyResistence);
            }
            else if (t == DamageType.physical)
            {
                defenseTemp = Random.Range(MyArmor * 0.1f, MyArmor);
            }

            float defensed = 1 - defenseTemp / 500;
            if (defensed < 0.1f) defensed = 0.1f;
            int damageTaken = Mathf.FloorToInt(damageEnemy * defensed);
            StartCoroutine("HitMaterialChange");
            CurrentLife = (damageTaken * -1);
            if (isCritical)
            {
                combatTextManager.CriticText(gameObject.transform, damageTaken);
            }
            else
            {

                combatTextManager.AttackText(gameObject.transform, damageTaken);
            }
        }
    }
    private IEnumerator HitMaterialChange()
    {
        render.material = materialHit;
        yield return new WaitForSeconds(0.2f);
        render.material = mat;
    }
    private IEnumerator DeadCourotine()
    {
        GetComponentInChildren<StatsBar>().gameObject.SetActive(false);
        body.SetActive(false);
        anim.enabled = false;
        col.enabled = false;
        agent.enabled = false;
        outline.enabled = false;
        Dead = true;
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
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


    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            if (!target)
            {
                target = col.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            if (col.gameObject == target)
            {
                target = null;
            }
        }
    }
    private IEnumerator AttackCountDown()
    {
        isReadyAttack = false;
        yield return new WaitForSeconds(AttackSpeed);
        isReadyAttack = true;
    }
}
