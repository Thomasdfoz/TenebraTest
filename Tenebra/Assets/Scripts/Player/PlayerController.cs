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
    private Animator playerAnim;


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
    #endregion

    public GameObject SelectedTarget { get => selectedTarget; set => selectedTarget = value; }
    public bool IsAttacking { get => isAttacking; }
    public bool IsLookTarget { get => isLookTarget; }
    public bool IsAnimationEnd { get => isAnimationEnd; set => isAnimationEnd = value; }

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = playerBody.GetComponent<Animator>();
        playerStats = GetComponent<PlayerStats>();
        joystickMoviment = gameController.ButtonsActive.moviment.GetComponent<FixedJoystick>();
        circleRange.SetActive(false);
        IsAnimationEnd = true;
        isReadyAttack = true;
        isAttacking = false;
        isLookTarget = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (SelectedTarget == null)
        {
            isAttacking = false;
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
        Move();
        Animations();
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
    private void Move()
    {
        float horizontal = joystickMoviment.Horizontal;
        float vertical = joystickMoviment.Vertical;
        float SpeedH = 0;
        float SpeedV = 0;
        float moveSpeed = 0;


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
            if (SpeedH > 0.70f) SpeedH = 1;
            moveSpeed = SpeedH * playerStats.MoveSpeed;

        }
        else
        {
            playerAnim.SetFloat("velocity", SpeedV);
            if (SpeedV > 0.70f) SpeedV = 1;
            moveSpeed = SpeedV * playerStats.MoveSpeed;
        }

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude > 0.1f)
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
    public void LookTarget(Transform lookTarget)
    {
        Vector3 targ = new Vector3(lookTarget.position.x, -0.5f, lookTarget.position.z);
        Vector3 direction = Vector3.RotateTowards(Vector3.forward, targ - playerBody.transform.position, 5f, 5f);
        playerBody.transform.rotation = Quaternion.Lerp(playerBody.transform.rotation, Quaternion.LookRotation(direction, Vector3.up), Time.deltaTime * 7f);
        if (!IsLookTarget)
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
                if (tar.gameObject.name == SelectedTarget.gameObject.name)
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
    public void AutoAttackMelee()
    {
        bool isCritical = false;
        float damage;
        if (Critic.IsCritic(playerStats.ChanceCritic))
        {
            damage = Random.Range(playerStats.Damage, playerStats.Damage * 2);
            isCritical = true;
        }
        else
        {
            damage = Random.Range((playerStats.Damage * 0.1f), playerStats.Damage);
        }
        SendDamage sendDamage = new SendDamage(Mathf.FloorToInt(playerStats.Damage), isCritical, playerStats.DamageType);
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
            damageFinal = SkillCalculator.Calcule(damage, playerStats.MagicSkill.CurrentLevel);
        }
        else if (damageType == DamageType.physical)
        {
            damageFinal = SkillCalculator.Calcule(damage, Mathf.FloorToInt(playerStats.Damage));
        }

        SendDamage sendDamage = new SendDamage(damageFinal, false, damageType);
        if (selectedTarget)
        {
            selectedTarget.SendMessage("TookDamage", sendDamage);
        }
    }

    public void SpecialAttackDistance(DamageType damageType, float damage)
    {
        SendDamage sendDamage = new SendDamage(Mathf.FloorToInt(damage + playerStats.Damage), false, damageType);
        if (selectedTarget)
        {
            selectedTarget.SendMessage("TookDamage", sendDamage);
        }
        StartCoroutine("CoroutineAttack");
    }


    #endregion
}
