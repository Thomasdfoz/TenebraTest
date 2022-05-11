using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsAndTextManager : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject moviment;
    public GameObject[] skills;
    public Button[] potions;
    public Button attack;
    public Button menu;
    public GameObject mapa;
    public GameObject changeSkills;
    public GameObject statsBar;
    public GameObject textDead;

    private Color colorBtnStandard;
    private Color colorBtnBlack = new Color(0, 0, 0, 0.7f);
    // Start is called before the first frame update
    void Start()
    {
        colorBtnStandard = moviment.GetComponent<Image>().color;
        Activatebtn(textDead, false);
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
            for (int i = 0; i < skills.Length; i++)
            {
                Activatebtn(skills[i], true);

            }
            for (int i = 0; i < potions.Length; i++)
            {
                Activatebtn(potions[i].gameObject, true);
            }
            Activatebtn(moviment, true);
            Activatebtn(moviment.GetComponentsInChildren<Image>()[1].gameObject, true);
            Activatebtn(attack.gameObject, true);
            Activatebtn(menu.gameObject, true);
            Activatebtn(mapa, true);
            Activatebtn(changeSkills, true);
            Activatebtn(statsBar, true);
            Activatebtn(textDead, false);

        }
        else
        {
            for (int i = 0; i < skills.Length; i++)
            {
                Activatebtn(skills[i], false);

            }
            for (int i = 0; i < potions.Length; i++)
            {
                Activatebtn(potions[i].gameObject, false);
            }
            Activatebtn(moviment, false);
            Activatebtn(attack.gameObject, false);
            Activatebtn(menu.gameObject, false);
            Activatebtn(mapa, false);
            Activatebtn(changeSkills, false);
            Activatebtn(statsBar, false);
            Activatebtn(textDead, true);
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
            for (int i = 0; i < skills.Length; i++)
            {
                skills[i].SetActive(true);
            }

        }
        else
        {
            for (int i = 0; i < skills.Length; i++)
            {
                skills[i].SetActive(false);
            }

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
            attack.GetComponent<AttackButton>().enabled = true;
            attack.GetComponent<Image>().color = colorBtnStandard;
        }
        else
        {
            attack.interactable = false;
            attack.GetComponent<AttackButton>().enabled = false;
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
        btn.SetActive(isTrue);
    }
    #endregion
}
