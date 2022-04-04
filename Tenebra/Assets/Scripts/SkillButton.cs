using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SkillButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    [Header("Main")]
    public GameController gameController;
    public Image areaEffect;
    public Image projectileEffect;
    public GameObject skillPrefab;
    public GameObject spanwPoint;
    public RectTransform[] limites;




    #region privates

    private bool isMoving;
    private bool isAreaSkill;
    private bool isProjectile;
    private float posY;
    private float posX;
    private float posZ;
    private Joystick joy;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        joy = GetComponentInChildren<FixedJoystick>();
        joy.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isAreaSkill)
        {
            MoveAreaSkill();
        }
        else if (isProjectile)
        {
            ProjectileRotation();
        }
    }
    public void OnPointerUp(PointerEventData data)
    {
        if (gameController.skill.skillType == SkillType.Area)
        {
            AreaSkillUp();
        }
        else if (gameController.skill.skillType == SkillType.Projectile)
        {
            ProjectileUp();
        }
        else if (gameController.skill.skillType == SkillType.AutoAttack)
        {

        }

        joy.OnPointerUp(data);
    }
    public void OnPointerDown(PointerEventData data)
    {
        if (gameController.skill.skillType == SkillType.Area)
        {
            AreaSkill();
        }
        else if (gameController.skill.skillType == SkillType.Projectile)
        {
            ProjectileSkill();
        }
        else if (gameController.skill.skillType == SkillType.AutoAttack)
        {
            AutoAttackSkill();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        joy.OnDrag(eventData);
        if (joy.Vertical > 0.3f || joy.Horizontal > 0.3f || joy.Vertical < -0.3f || joy.Horizontal < -0.3f)
        {
            if (isAreaSkill)
            {
                areaEffect.gameObject.SetActive(true);
            }
            else if (isProjectile)
            {
                projectileEffect.gameObject.SetActive(true);
            }
        }
    }
    #region Area skill Methods
    private void AreaSkill()
    {
        posY = 0;
        posX = 0;
        posZ = 0;
        areaEffect.sprite = gameController.skill.imageEffect;
        areaEffect.rectTransform.sizeDelta = new Vector2(gameController.skill.width, gameController.skill.height);
        skillPrefab = gameController.skill.prefabEffect;
        GetComponent<Image>().enabled = false;
        joy.gameObject.SetActive(true);
        isMoving = true;
        isAreaSkill = true;
    }
    private void MoveAreaSkill()
    {
        if (isMoving)
        {
            posX += (joy.Horizontal / 4);
            posZ += (joy.Vertical / 4);

            if (posX > limites[3].anchoredPosition3D.x)
            {
                posX = limites[3].anchoredPosition3D.x;
            }
            if (posX < limites[1].anchoredPosition3D.x)
            {
                posX = limites[1].anchoredPosition3D.x;
            }
            if (posZ > limites[2].anchoredPosition3D.z)
            {
                posZ = limites[2].anchoredPosition3D.z;
            }
            if (posZ < limites[0].anchoredPosition3D.z)
            {
                posZ = limites[0].anchoredPosition3D.z;

            }
            areaEffect.rectTransform.anchoredPosition3D = new Vector3(posX, posY, posZ);
        }
        Vector3 difference = areaEffect.transform.position - gameController.player.transform.position;
        float rotationZ = Mathf.Atan2(difference.z, difference.x) * Mathf.Rad2Deg;
        areaEffect.transform.rotation = Quaternion.Euler(90.0f, 0.0f, rotationZ - 90f);
    }
    private void AreaSkillUp()
    {
        areaEffect.gameObject.SetActive(false);
        GetComponent<Image>().enabled = true;
        joy.gameObject.SetActive(false);
        GameObject magic = Instantiate(skillPrefab);
        Vector3 pos = new Vector3(areaEffect.rectTransform.position.x, skillPrefab.transform.position.y, areaEffect.rectTransform.position.z);
        magic.transform.position = pos;

        isAreaSkill = false;
        isMoving = false;
        posY = 0;
        posX = 0;
        posZ = 0;
    }
    #endregion
    #region Projectile Skill Methods
    private void ProjectileSkill()
    {
        projectileEffect.sprite = gameController.skill.imageEffect;
        areaEffect.rectTransform.sizeDelta = new Vector2(gameController.skill.width, gameController.skill.height);
        skillPrefab = gameController.skill.prefabEffect;
        GetComponent<Image>().enabled = false;
        joy.gameObject.SetActive(true);
        isProjectile = true;

    }
    private void ProjectileRotation()
    {
        Vector3 direction = new Vector3(joy.Horizontal, 0f, joy.Vertical).normalized;
        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
            projectileEffect.rectTransform.rotation = Quaternion.Euler(90, 0, targetAngle);
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            spanwPoint.transform.rotation = Quaternion.Euler(0, targetAngle, 0);
        }
    }
    private void ProjectileUp()
    {
        projectileEffect.gameObject.SetActive(false);
        GetComponent<Image>().enabled = true;
        joy.gameObject.SetActive(false);
        GameObject missile = Instantiate(skillPrefab, spanwPoint.transform.position, spanwPoint.transform.rotation);

        missile.GetComponent<Rigidbody>().velocity = (spanwPoint.transform.forward * 3);
    }
    #endregion
    #region Auto Attack Skill Methods
    private void AutoAttackSkill()
    {
        //essa animaçao vai estar dentro do scriptable tambem "animSpecialAttack"
        if (gameController.playerController.IsLookTarget && gameController.playerController.IsAttacking)
        {
            gameController.playerController.CallSpecialAttack(300, DamageType.magic, WaeponType.melee, 0.33f, gameController.animSpecialAttack);
        }
        else
        {
            Debug.Log("Nenhum Alvo Selecionado");
        }
    }
    #endregion
}
