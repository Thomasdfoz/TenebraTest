using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum SkillBtn
{
    Auto,
    Area,
    Projectile
}
public class SkillButton3 : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler, IDragHandler
{
    public GameObject player;
    public SkillBtn[] skillBtn;
    public GameObject magicAutoPrefab;
    public int Button;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
    public void OnPointerUp(PointerEventData data)
    {

    }
    public void OnPointerDown(PointerEventData data)
    {
        if (skillBtn[Button - 1] == SkillBtn.Auto)
        {
            ButtonDownAutoSkill();
        }
        else if (skillBtn[Button - 1] == SkillBtn.Area)
        {
            ButtonDownAreaSkill();
        }
        else if (skillBtn[Button -1] == SkillBtn.Projectile)
        {
            ButtonDownProjectSkill();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

    }
    #region Auto Skill
    private void ButtonDownAutoSkill()
    {
        player.GetComponent<PlayerMoviment>().SpecialAttack(300, DamageType.magic);
    }
    #endregion
    #region Area Skill
    private void ButtonDownAreaSkill()
    {

    }
    private void ButtonUpAreaSkill()
    {

    }
    private void ButtonDragAreaSkill()
    {

    }
    #endregion
    #region Project Skill
    private void ButtonDownProjectSkill()
    {

    }
    private void ButtonUpProjectSkill()
    {

    }
    private void ButtonDragProjectSkill()
    {

    }
    #endregion

    public void OnPointerClick(PointerEventData data)
    {


    }
}
