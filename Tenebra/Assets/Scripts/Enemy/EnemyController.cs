using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Material materialHit;
    [SerializeField] SkinnedMeshRenderer render;
    [SerializeField] private int life;
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
    public int Life { 
        get => life; 
        set {
            life += value;
            if (life < 0)
            {
                life = 0;
                dead = true;
            }
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponentInChildren<SkinnedMeshRenderer>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider>();
        outline = GetComponent<Outline>();
        agent = GetComponent<NavMeshAgent>();
        mat = render.material;
     }

    // Update is called once per frame
    void Update()
    {
        if (Life == 0)
        {
            StartCoroutine("Dead");
        }
    }
    public void TookDamage(SendDamage sendDamage)
    {

        int damageEnemy = sendDamage.Damage;
        DamageType t = sendDamage.DamageType;
        bool isCritical = sendDamage.IsCritical;
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
            damage = damageEnemy;
            if (isCritical)
            {
                Debug.Log("Critico");
            }
            defenseTemp = Random.Range(MyArmor * 0.1f, MyArmor);
        }

        defensed = 1 - (defenseTemp / 500);
        if (defensed < 0.1f) defensed = 0.1f;
        damageTaken = Mathf.FloorToInt(damage * defensed);
        StartCoroutine("HitMaterialChange");
        Life = (damageTaken * -1);
        Debug.Log(damageTaken + ", de dano tomado. " + (1 - defensed).ToString("P") + " defendido, dano inimigo " + damage + " defesa ," + defenseTemp.ToString("F0") + ", Tipo de dano: " + t);

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
