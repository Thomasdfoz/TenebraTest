using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public CombatTextManager combatTextManager;
    [SerializeField] private Material materialHit;
    [SerializeField] SkinnedMeshRenderer render;
    [SerializeField] private float maxLife;
    private float currentLife;
    [SerializeField] private int damage;
    [SerializeField] private float myArmor;
    [SerializeField] private float myResistence;
    [SerializeField] private DamageType damageType;
    [SerializeField] private GameObject body;
    private Animator anim;
    private Collider col;
    private NavMeshAgent agent;
    private Outline outline;





    private Material mat;
    private bool dead;

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

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponentInChildren<SkinnedMeshRenderer>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider>();
        outline = GetComponent<Outline>();
        agent = GetComponent<NavMeshAgent>();
        mat = render.material;
        CurrentLife = MaxLife;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLife == 0)
        {
            StartCoroutine("Dead");
        }
    }
    public void TookDamage(SendDamage sendDamage)
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
        Debug.Log(damageTaken + ", de dano tomado. " + (1 - defensed).ToString("P") + " defendido, dano inimigo " + damageEnemy + " defesa ," + defenseTemp.ToString("F0") + ", Tipo de dano: " + t);

    }
    private IEnumerator HitMaterialChange()
    {
        render.material = materialHit;
        yield return new WaitForSeconds(0.2f);
        render.material = mat;
    }
    private IEnumerator Dead()
    {
        body.SetActive(false);
        anim.enabled = false;
        col.enabled = false;
        agent.enabled = false;
        outline.enabled = false;
        dead = true;
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
}
