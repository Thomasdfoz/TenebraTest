using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoviment : MonoBehaviour
{
    [Header("Main")]
    public GameObject PlayerMain;
    public AnimationClip animSpecialAttack;
    public BuffedManager buffedManager;
    [Header("Selected")]
    public GameObject selectedTarget;
    [Range(1.5f, 8f)]
    [SerializeField] private float range;
    public Material selectedMaterial;
    private Collider[] colls;



    [Header("Mira")]
    public float miraX;
    public float miraY;
    public Transform[] limites;
    public Image mira;
    public bool miraBolean;
    public GameObject arcoRange;

    [Header("Joysticks")]
    public FixedJoystick joystick;
    public FixedJoystick joystickMira;

    [Header("Propriedades")]
    public WaeponType waeponType;
    public DamageType damageType;
    [SerializeField] private float myLife;
    [SerializeField] private float myMana;
    [SerializeField] private float myDamage;
    [SerializeField] private float myArmor;
    [SerializeField] private float myResistence;
    [SerializeField] private float mySkillMelee;
    [SerializeField] private float mySkillDistance;
    [SerializeField] private float mySkillDefense;
    [SerializeField] private float mySkillMagic;
    [SerializeField] private float myMoveSpeed;
    [SerializeField] private bool chanceCritic;
    [SerializeField] private bool isSpecialAttack;

    [Range(25, 200)] public float myAttackSpeed;
    private float moveSpeed = 0;
    public LayerMask layer;
    private bool isWalk;
    public CharacterController controller;
    private Animator playerAnim;
    private RaycastHit hit;
    private bool attacking;
    private bool readyAttack;
    public bool isLookTarget;
    private Vector3 direction;
    private float attackSpeedAnim;

    #region getters and Setters
    public float MyAttackSpeed
    {
        get => (1 / (myAttackSpeed / 100));
        set
        {
            if (value > 200)
            {
                myAttackSpeed = 200;
            }
            else if (value < 25)
            {
                myAttackSpeed = 25;
            }
            else
            {
                myAttackSpeed = value;
            }

        }
    }
    public float MyArmor
    {
        get => ((MySkillDefense / 100) * myArmor) + myArmor;
        set
        {
            myArmor += value;
            if (myArmor < 10)
            {
                myArmor = 10;
            }
        }
    }
    public float MyDamage
    {
        get
        {
            if (waeponType == WaeponType.melee)
            {
                return (myDamage * (MySkillMelee / 100)) + myDamage;
            }
            else if (waeponType == WaeponType.distance)
            {
                return ((MySkillDistance / 100) * myDamage) + myDamage;
            }
            else if (waeponType == WaeponType.magic)
            {
                return ((MySkillMagic / 100) * myDamage) + myDamage;
            }
            else
            {
                return ((MySkillMelee / 100) * 2) + 5;
            }
        }

        set
        {
            myDamage += value;
            if (myDamage < 1)
            {
                myDamage = 1;
            }
        }
    }
    public float MyResistence
    {
        get => myResistence;
        set
        {
            myResistence += value;
            if (myResistence < 10)
            {
                myResistence = 10;
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

    public float MySkillMelee
    {
        get => mySkillMelee;
        set
        {
            mySkillMelee += value;
            if (mySkillMelee < 10)
            {
                mySkillMelee = 10;
            }
        }
    }
    public float MySkillDistance
    {
        get => mySkillDistance;
        set
        {
            mySkillDistance += value;
            if (mySkillDistance < 10)
            {
                mySkillDistance = 10;
            }
        }
    }
    public float MySkillDefense
    {
        get => mySkillDefense;
        set
        {
            mySkillDefense += value;
            if (mySkillDefense < 10)
            {
                mySkillDefense = 10;
            }
        }
    }
    public float MySkillMagic
    {
        get => mySkillMagic;
        set
        {
            mySkillMagic += value;
            if (mySkillMagic < 10)
            {
                mySkillMagic = 10;
            }
        }
    }
    public float MyLife
    {
        get => myLife;
        set
        {
            myLife += value;
            if (myLife < 0)
            {
                myLife = 0;
            }
        }
    }
    public float MyMana
    {
        get => myMana;
        set
        {
            myMana += value;
            if (myMana < 0)
            {
                myMana = 0;
            }
        }
    }
    public float MyMoveSpeed
    {
        get => myMoveSpeed;
        set
        {
            myMana += value;
            if (myMana < 1)
            {
                myMana = 1;
            }
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerAnim = GetComponent<Animator>();
        miraX = mira.transform.position.x;
        miraY = mira.transform.position.y;
        arcoRange.SetActive(false);
        readyAttack = true;

    }
   // Update is called once per frame
    void FixedUpdate()
    {
        arcoRange.transform.localScale = new Vector3(range, range, 1);
        attackSpeedAnim = myAttackSpeed / 100;
        if (myAttackSpeed < 80)
        {
            attackSpeedAnim = 0.8f;
        }
        playerAnim.SetFloat("attackSpeed", attackSpeedAnim);
        playerAnim.SetBool("isWalk", isWalk);
        Move();

        if (miraBolean)
        {
            miraX += (joystickMira.Horizontal * 3);
            miraY += (joystickMira.Vertical * 3);

            if (miraX > limites[3].position.x)
            {
                miraX = limites[3].position.x;
            }
            if (miraX < limites[1].position.x)
            {
                miraX = limites[1].position.x;
            }
            if (miraY > limites[2].position.y)
            {
                miraY = limites[2].position.y;
            }
            if (miraY < limites[0].position.y)
            {
                miraY = limites[0].position.y;
            }
            mira.rectTransform.position = new Vector3(miraX, miraY, mira.transform.position.z);
        }
        else
        {
            miraX = mira.transform.position.x;
            miraY = mira.transform.position.y;
        }
        if (selectedTarget == null)
        {
            attacking = false;
        }
        if (attacking)
        {


            LookTarget();
            if (readyAttack && isLookTarget)
            {
                playerAnim.SetTrigger("attack");
                StartCoroutine("CoroutineAttack");
            }
        }

    }
    public void LookTarget()
    {
        Vector3 targ = new Vector3(selectedTarget.transform.position.x, -0.5f, selectedTarget.transform.position.z);
        Vector3 direction = Vector3.RotateTowards(Vector3.forward, targ - transform.position, 5f, 5f);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), Time.deltaTime * 7f);
        if (!isLookTarget)
        {
            StartCoroutine("LookTargetTime", 0.3f);
        }


    }
    private IEnumerator LookTargetTime(float time)
    {
        isLookTarget = false;
        yield return new WaitForSeconds(time);
        isLookTarget = true;
    }
    public void SelectTarget(GameObject target)
    {
        selectedTarget = target;
    }
    public void RangedSelectTarget()
    {
        selectedTarget = SelectedingSphere();
    }
    public GameObject SelectedingSphere()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, range, layer);
        colls = targets;
        GameObject t = null;
        float minDistance = 999;
        foreach (Collider tar in targets)
        {
            if (selectedTarget != null)
            {
                if (tar.gameObject.name == selectedTarget.gameObject.name)
                {
                    t = selectedTarget;
                    return t;
                }
            }
            if (Vector3.Distance(transform.position, tar.transform.position) < minDistance)
            {
                t = tar.gameObject;
                minDistance = Vector3.Distance(transform.position, tar.transform.position);
            }
        }
        return t;
    }
    public void AttackSelected()
    {
        if (selectedTarget != null)
        {
            selectedTarget.GetComponent<Outline>().OutlineWidth = 2;
        }
        attacking = true;
    }


    private IEnumerator CoroutineAttack()
    {
        readyAttack = false;
        yield return new WaitForSeconds(MyAttackSpeed);
        readyAttack = true;
    }
    public void SpecialAttack(float damage, DamageType damageType)
    {
        string parms = damageType.ToString() + "/" + damage.ToString();
        if (animSpecialAttack.events.Length <= 0)
        {
            AnimationEvent AnimEvent = new AnimationEvent();
            AnimEvent.functionName = "AutoAttackSpecial";
            AnimEvent.time = 0.33f;
            AnimEvent.stringParameter = parms;
            animSpecialAttack.AddEvent(AnimEvent);
        }
        else
        {
            animSpecialAttack.events[0].stringParameter = parms;
        }
        StopCoroutine("CoroutineAttack");
        playerAnim.SetTrigger("specialAttack");
    }
    private void AutoAttackSpecial(string parms)
    {
        string[] cut = parms.Split('/');
        string StringType = cut[0];
        float damage = float.Parse(cut[1]);
        DamageType damageType;

        if (StringType == DamageType.physical.ToString())
        {
            damageType = DamageType.physical;
        }
        else
        {
            damageType = DamageType.magic;
        }

        SendDamage sendDamage = new SendDamage(Mathf.FloorToInt(damage), true, damageType);
        if (selectedTarget)
        {
            selectedTarget.SendMessage("TookDamage", sendDamage);
        }
        StartCoroutine("CoroutineAttack");
    }
    public void AutoAttackMelee()
    {
        SendDamage sendDamage = new SendDamage(Mathf.FloorToInt(MyDamage), chanceCritic, damageType);
        if (selectedTarget)
        {
            selectedTarget.SendMessage("TookDamage", sendDamage);
        }
    }

    public void Move()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
        float SpeedH = 0;
        float SpeedV = 0;

        if (horizontal < 0)
        {
            SpeedH = horizontal * -1;
        }
        else
        {
            SpeedH = horizontal;
        }

        if (vertical < 0)
        {
            SpeedV = vertical * -1;
        }
        else
        {
            SpeedV = vertical;
        }

        if (SpeedH > SpeedV)
        {
            playerAnim.SetFloat("velocity", SpeedH);
            if (SpeedH > 0.70f)
            {
                SpeedH = 1;
            }
            moveSpeed = SpeedH * MyMoveSpeed;

        }
        else
        {
            playerAnim.SetFloat("velocity", SpeedV);
            if (SpeedV > 0.70f)
            {
                SpeedV = 1;
            }
            moveSpeed = SpeedV * MyMoveSpeed;
        }

        direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);
            isWalk = true;
            attacking = false;
            isLookTarget = false;
            if (selectedTarget != null)
            {
                if (Vector3.Distance(transform.position, selectedTarget.transform.position) > range)
                {

                    selectedTarget.GetComponent<Outline>().OutlineWidth = 0;
                }
            }

        }
        else
        {
            isWalk = false;
        }

        controller.Move(direction * moveSpeed * Time.deltaTime);

    }


    public void TookDamage(SendDamage sendDamage)
    {

        int damageEnemy = sendDamage.Damage;
        DamageType t = sendDamage.DamageType;
        bool criticalChance = sendDamage.IsCritical;
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

            if (criticalChance)
            {
                Debug.Log("Critico");
            }

            defenseTemp = Random.Range(MyArmor * 0.1f, MyArmor);
        }

        defensed = 1 - (defenseTemp / 500);
        if (defensed < 0.1f) defensed = 0.1f;
        damageTaken = Mathf.FloorToInt(damage * defensed);
        Debug.Log(damageTaken + ", de dano tomado. " + (1 - defensed) * 100 + "% defendido, dano inimigo " + damage + " defesatemp ," + defenseTemp + " My," + MyDamage);
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
