using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Joysticks")]
    public FixedJoystick joystick;
    [Header("Main")]
    public PlayerStats playerStats;
    public LayerMask layer;
    public Animator playerAnim;


    private GameObject selectedTarget;
    private bool isWalk;
    public CharacterController controller;
    private RaycastHit hit;
    private bool attacking;
    private bool readyAttack;
    private bool isLookTarget;
    private float attackSpeedAnim;

    public GameObject SelectedTarget { get => selectedTarget; set => selectedTarget = value; }










    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        readyAttack = true;
        attacking = false;
        isLookTarget = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (SelectedTarget == null)
        {
            attacking = false;
        }
        if (attacking)
        {
            LookTarget(SelectedTarget.transform);
            if (readyAttack && isLookTarget)
            {
                SetTrigger("attack");
                StartCoroutine("CoroutineAttack");
            }
        }
        Move();
        Animations();
    }
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

    private void Move()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
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
            if (SpeedH > 0.70f)
            {
                SpeedH = 1;
            }
            moveSpeed = SpeedH * playerStats.MoveSpeed;

        }
        else
        {
            playerAnim.SetFloat("velocity", SpeedV);
            if (SpeedV > 0.70f)
            {
                SpeedV = 1;
            }
            moveSpeed = SpeedV * playerStats.MoveSpeed;
        }

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);
            isWalk = true;
            attacking = false;
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
    public void RangedSelectTarget()
    {
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
        if (SelectedTarget != null)
        {
            SelectedTarget.GetComponent<Outline>().OutlineWidth = 2;
        }
        attacking = true;
    }
    private IEnumerator CoroutineAttack()
    {
        readyAttack = false;
        yield return new WaitForSeconds(playerStats.AttackSpeed);
        readyAttack = true;
    }
}
