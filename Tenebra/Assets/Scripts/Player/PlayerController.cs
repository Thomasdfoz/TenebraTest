using System.Collections;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    #region ------------------------Variables-------------------
    [Header("GameController")]
    public GameController gameController;

    [Header("Main")]
    public GameObject playerBody;
    public GameObject circleRange;
    public LayerMask layer;
    private PlayerStats playerStats;
    public Animator playerAnim;

    

    private FixedJoystick joystickMoviment;
    public GameObject selectedTarget;
    private bool isWalk;
    public CharacterController controller;
    private RaycastHit hit;
    private bool isAttacking;
    private bool isReadyAttack;
    private bool isAnimationEnd;
    private bool isLookTarget;
    private float attackSpeedAnim;

    private Transform lookSkill;
    private bool isLookTargetSkill = false;
    private float gravity;

    #endregion

    public GameObject SelectedTarget { get => selectedTarget; set => selectedTarget = value; }
    public bool IsAttacking { get => isAttacking; }
    public bool IsLookTarget { get => isLookTarget; }
    public bool IsAnimationEnd { get => isAnimationEnd; set => isAnimationEnd = value; }

    // Start is called before the first frame update
    void Start()
    {
        
        playerStats = GetComponent<PlayerStats>();
        joystickMoviment = gameController.ButtonsActive.moviment.GetComponent<FixedJoystick>();
        circleRange.SetActive(false);
        IsAnimationEnd = true;
        isReadyAttack = true;
        isAttacking = false;
        isLookTarget = false;
    }
    void LateUpdate()
    {
        playerAnim = playerBody.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (controller.isGrounded) gravity = 0;
        else if(!controller.isGrounded && isWalk)
        {
            gravity -= 9.81f * Time.deltaTime;
        }
        if (SelectedTarget == null)
        {
            isAttacking = false;
        }
        else
        {
            if (selectedTarget.GetComponent<EnemyController>().Dead)
            {
                StopAllCoroutines();
                isReadyAttack = false;
                isAttacking = false;
                selectedTarget = null;
            }
        }
        if (IsAttacking)
        {
            LookTarget(SelectedTarget.transform);
            if (isReadyAttack && IsLookTarget)
            {
                IsAnimationEnd = false;
                SetTrigger("attack");
                StopCoroutine("CoroutineAttack");
                StartCoroutine("CoroutineAttack");

            }
        }
        if (isLookTargetSkill)
        {
            LookTarget(lookSkill);
        }
        if (playerAnim != null)
        {
            Move();
            Animations();
        }
    }
    #region ----------------------My Functions------------------
    private void Animations()
    {
        attackSpeedAnim = playerStats.AttackSpeed / 100;
        if (playerStats.AttackSpeed < 80)
        {
            attackSpeedAnim = 0.8f;
        }
        playerAnim.SetFloat("attackSpeed", attackSpeedAnim);
        playerAnim.SetBool("isWalk", isWalk);
    }
    public void SetTrigger(string trigger)
    {
        playerAnim.SetTrigger(trigger);
    }
    public void SetTrigger(string trigger, float time)
    {
        string temp = trigger + "/" + time.ToString();
        StartCoroutine("SetTriggerDelay", temp);
    }
    public void AnimationEnd()
    {
        IsAnimationEnd = true;
    }
    IEnumerator SetTriggerDelay(string temp)
    {
        string[] arg = temp.Split("/");

        yield return new WaitForSeconds(float.Parse(arg[1]));
        playerAnim.SetTrigger(arg[0]);
    }
    public void LookTarget(Transform lookTarget)
    {
        Vector3 targ = new(lookTarget.position.x, -0.5f, lookTarget.position.z);
        Vector3 direction = Vector3.RotateTowards(Vector3.forward, targ - playerBody.transform.position, 5f, 5f);
        playerBody.transform.rotation = Quaternion.Lerp(playerBody.transform.rotation, Quaternion.LookRotation(direction, Vector3.up), Time.deltaTime * 10f);
        if (!IsLookTarget && IsAttacking)
        {
            StartCoroutine("LookTargetTime", 0.3f);
        }
    }
    /// <summary>
    /// Olha para o alvo e depois de um tempo ele executa o trigger.
    /// </summary>
    /// <param name="target">Esse é o alvo para onde o player ira olhar</param>
    /// <param name="timeLook"> Esse é o tempo que o jogador vai esperar antes de executar o trigger</param>
    /// <param name="triggerName">Esse é o trigger da animacao que vai chamar la no Animator</param>    
    public void SkillAnimation(Transform target, float timeLook, string triggerName)
    {

        isLookTargetSkill = true;
        lookSkill = target;
        string p = (timeLook.ToString() + "/" + triggerName);
        StartCoroutine(LookTargetTime(p));
    }
    /// <summary>
    /// Nâo olha para o avo, espera um tempo e depois executa o trigger, 
    /// </summary>
    /// <param name="timeLook">Esse é o tempo que o jogador vai esperar antes de executar o trigger</param>
    /// <param name="triggerName">Esse é o trigger da animacao que vai chamar la no Animator</param>
    public void SkillAnimation(float timeLook, string triggerName)
    {
        string p = (timeLook.ToString() + "/" + triggerName);
        StartCoroutine("LookTargetTime", p);
    }
    private IEnumerator LookTargetTime(float time)
    {
        isLookTarget = false;
        yield return new WaitForSeconds(time);
        isLookTarget = true;
    }
    private IEnumerator LookTargetTime(string p)
    {
        string[] parm = p.Split("/");
        isLookTarget = false;
        yield return new WaitForSeconds(float.Parse(parm[0]));
        SetTrigger(parm[1], 0.1f);
        isLookTarget = true;
        isLookTargetSkill = false;
    }
    public void RangedSelectTarget()
    {
        circleRange.transform.localScale = new Vector3(playerStats.Range, playerStats.Range, 1);
        SelectedTarget = SelectedingSphere();
    }
    public GameObject SelectedingSphere()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, playerStats.Range, layer);
        GameObject t = null;
        float minDistance = 999;
        foreach (Collider tar in targets)
        {
            if (SelectedTarget != null)
            {
                if (tar.gameObject.name == SelectedTarget.name)
                {
                    t = SelectedTarget;
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
        OutlineEnemy();
        isAttacking = true;
    }
    public void ReadySpecialAttack()
    {
        StopCoroutine("CoroutineAttack");
        StartCoroutine("CoroutineAttack", 0.1f);
    }
    public void OutlineEnemy()
    {
        if (SelectedTarget != null)
        {
            SelectedTarget.GetComponent<Outline>().OutlineWidth = 2;
        }
    }
    private IEnumerator CoroutineAttack()
    {
        isReadyAttack = false;
        yield return new WaitForSeconds(playerStats.AttackSpeed);
        isReadyAttack = true;
    }
    private IEnumerator CoroutineAttack(float time)
    {
        isReadyAttack = false;
        yield return new WaitForSeconds(playerStats.AttackSpeed + time);
        isReadyAttack = true;
    }
    public void AutoAttackDistance()
    {

    }
    public void AutoAttackMelee()
    {
        bool isCritical = Critic.IsCritic(playerStats.ChanceCritic);
        float damage;
        if (isCritical)
        {
            damage = SkillCalculator.CalculeCriticAttack(playerStats.Damage);
        }
        else
        {
            damage = SkillCalculator.CalculeNormalAttack(playerStats.Damage);
        }
        SendDamage sendDamage = new(Mathf.RoundToInt(damage), isCritical, playerStats.DamageType);
        if (selectedTarget)
        {
            selectedTarget.SendMessage("TookDamage", sendDamage);
        }
    }
    public void SpecialAttackMelee(DamageType damageType, float damage)
    {
        int damageFinal = 0;
        if (damageType == DamageType.magic)
        {
            damageFinal = SkillCalculator.CalculeAbility(damage, playerStats.MagicSkill.CurrentLevel);
        }
        else if (damageType == DamageType.physical)
        {
            damageFinal = SkillCalculator.CalculeAbility(damage, playerStats.MeleeSkill.CurrentLevel);
        }

        SendDamage sendDamage = new(damageFinal, false, damageType);
        if (selectedTarget)
        {
            selectedTarget.SendMessage("TookDamage", sendDamage);
        }
    }
    public void SpecialAttackDistance(DamageType damageType, float damage)
    {
        SendDamage sendDamage = new(Mathf.RoundToInt(damage + playerStats.Damage), false, damageType);
        if (selectedTarget)
        {
            selectedTarget.SendMessage("TookDamage", sendDamage);
        }
        StartCoroutine("CoroutineAttack");
    }
    private void Move()
    {
        float horizontal = joystickMoviment.Horizontal;
        float vertical = joystickMoviment.Vertical;
        float SpeedH;
        if (horizontal < 0)
        {
            SpeedH = horizontal * -1;
        }
        else
        {
            SpeedH = horizontal;
        }

        float SpeedV;
        if (vertical < 0)
        {
            SpeedV = vertical * -1;
        }
        else
        {
            SpeedV = vertical;
        }
        float moveSpeed;
        if (SpeedH > SpeedV)
        {
            playerAnim.SetFloat("velocity", SpeedH);
            if (SpeedH > 0.70f) SpeedH = 1;
            moveSpeed = SpeedH * playerStats.MoveSpeed;

        }
        else
        {
            playerAnim.SetFloat("velocity", SpeedV);
            if (SpeedV > 0.70f) SpeedV = 1;
            moveSpeed = SpeedV * playerStats.MoveSpeed;
        }

        Vector3 direction = new Vector3(horizontal, gravity, vertical);
        if (direction.x != 0 || direction.z != 0)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            playerBody.transform.rotation = Quaternion.Lerp(playerBody.transform.rotation, Quaternion.Euler(0, targetAngle, 0), Time.deltaTime * 7f);
            isWalk = true;
            isAttacking = false;
            isLookTarget = false;
            if (SelectedTarget != null)
            {
                if (Vector3.Distance(transform.position, SelectedTarget.transform.position) > playerStats.Range)
                {

                    SelectedTarget.GetComponent<Outline>().OutlineWidth = 0;
                }
            }
        }
        else
        {
            isWalk = false;
        }
        controller.Move(direction * moveSpeed * Time.deltaTime);
    }


    #endregion
}
