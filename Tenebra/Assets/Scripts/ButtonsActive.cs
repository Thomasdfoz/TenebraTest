using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsActive : MonoBehaviour
{
    [Header("Buttons")]
    public GameObject moviment;
    public Button[] skills;
    public Button[] potions;
    public Button attack;
    public Button menu;
    public GameObject changeSkill;

    private Color colorBtnStandard;
    private Color colorBtnBlack = new Color(0, 0, 0, 0.7f);
    // Start is called before the first frame update
    void Start()
    {
        colorBtnStandard = moviment.GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #region ----------------Enable and Disable Buttons---------------
    public void ActiveAll(bool isTrue)
    {
        if (isTrue)
        {
            foreach (var btn in skills)
            {
                Activatebtn(btn.gameObject, true);
            }
            foreach (var btn in potions)
            {
                Activatebtn(btn.gameObject, true);
            }
            Activatebtn(moviment, true);
            Activatebtn(moviment.GetComponentsInChildren<Image>()[1].gameObject, true);
            Activatebtn(attack.gameObject, true);
            Activatebtn(menu.gameObject, true);
        }
        else
        {
            foreach (var btn in skills)
            {
                Activatebtn(btn.gameObject, false);
            }
            foreach (var btn in potions)
            {
                Activatebtn(btn.gameObject, false);
            }
            Activatebtn(moviment, false);
            Activatebtn(moviment.GetComponentsInChildren<Image>()[1].gameObject, false);
            Activatebtn(attack.gameObject, false);
            Activatebtn(menu.gameObject, false);
        }
    }
    public void InteractableBtns(bool isTrue)
    {
        if (isTrue)
        {
            InteractableBtnSkills(true);
            InteractableBtnAttack(true);
            InteractableBtnMoviment(true);
            InteractableBtnPotions(true);
        }
        else
        {
            InteractableBtnSkills(false);
            InteractableBtnAttack(false);
            InteractableBtnMoviment(false);
            InteractableBtnPotions(false);
        }
       
    }
    
    public void InteractableBtnSkills(bool isTrue)
    {
        if (isTrue)
        {
            skills[0].interactable = true;
            skills[0].GetComponent<SkillButton>().enabled = true;
            skills[0].GetComponent<Image>().color = colorBtnStandard;
            skills[1].interactable = true;
            skills[1].GetComponent<SkillButton2>().enabled = true;
            skills[1].GetComponent<Image>().color = colorBtnStandard;
            skills[2].interactable = true;
            skills[2].GetComponent<SkillButton3>().enabled = true;
            skills[2].GetComponent<Image>().color = colorBtnStandard;
            skills[3].interactable = true;
            skills[3].GetComponent<Image>().color = colorBtnStandard;
        }
        else
        {
            skills[0].interactable = false;
            skills[0].GetComponent<SkillButton>().enabled = false;
            skills[0].GetComponent<Image>().color = colorBtnBlack;
            skills[1].interactable = false;
            skills[1].GetComponent<SkillButton2>().enabled = false;
            skills[1].GetComponent<Image>().color = colorBtnBlack;
            skills[2].interactable = false;
            skills[2].GetComponent<SkillButton3>().enabled = false;
            skills[2].GetComponent<Image>().color = colorBtnBlack;
            skills[3].interactable = false;
            skills[3].GetComponent<Image>().color = colorBtnBlack;
        }   
    }
   
    public void InteractableBtnMoviment(bool isTrue)
    {
        if (isTrue)
        {
            moviment.GetComponent<FixedJoystick>().enabled = true;
            moviment.GetComponent<Image>().color = colorBtnStandard;
            moviment.GetComponentInChildren<Image>().color = colorBtnStandard;
            Image hand = moviment.GetComponentsInChildren<Image>()[1];
            hand.color = colorBtnStandard;
        }
        else
        {
            moviment.GetComponent<FixedJoystick>().enabled = false;
            moviment.GetComponent<Image>().color = colorBtnBlack;
            Image hand = moviment.GetComponentsInChildren<Image>()[1];
            hand.color = colorBtnBlack;
        }
    }
    public void InteractableBtnAttack(bool isTrue)
    {
        if (isTrue)
        {
            attack.interactable = true;
            attack.GetComponent<ButtonDown>().enabled = true;
            attack.GetComponent<Image>().color = colorBtnStandard;
        }
        else
        {
            attack.interactable = false;
            attack.GetComponent<ButtonDown>().enabled = false;
            attack.GetComponent<Image>().color = colorBtnBlack;
        }
    }
    public void InteractableBtnPotions(bool isTrue)
    {
        if (isTrue)
        {
            potions[0].interactable = true;
            potions[0].GetComponent<Image>().color = colorBtnStandard;
            potions[1].interactable = true;
            potions[1].GetComponent<Image>().color = colorBtnStandard;
        }
        else
        {
            potions[0].interactable = false;
            potions[0].GetComponent<Image>().color = colorBtnBlack;
            potions[1].interactable = false;
            potions[1].GetComponent<Image>().color = colorBtnBlack;
        }
    }
    public void Activatebtn(GameObject btn, bool isTrue)
    {
        btn.GetComponent<Image>().enabled = isTrue;
    }
    #endregion
}
