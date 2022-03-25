using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject btnMoviment;
    public Button[] btnSkills;
    public Button[] btnPotions;
    public Button btnAttack;

    private Color colorBtnStandard;
    private Color colorBtnBlack = new Color(0, 0, 0, 0.7f);

    // Start is called before the first frame update
    void Start()
    {
        colorBtnStandard = btnMoviment.GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            DisableBtnSkills();
            DisableBtnAttack();
            DisableBtnMoviment();
            DisableBtnPotions();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            EnableBtnAttack();
            EnableBtnMoviment();
            EnableBtnPotions();
            EnableBtnSkills();
        }
    }
    public void DisableBtnSkills()
    {
        btnSkills[0].interactable = false;
        btnSkills[0].GetComponent<SkillButton>().enabled = false;
        btnSkills[0].GetComponent<Image>().color = colorBtnBlack;
        btnSkills[1].interactable = false;
        btnSkills[1].GetComponent<SkillButton2>().enabled = false;
        btnSkills[1].GetComponent<Image>().color = colorBtnBlack;
        btnSkills[2].interactable = false;
        btnSkills[2].GetComponent<SkillButton3>().enabled = false;
        btnSkills[2].GetComponent<Image>().color = colorBtnBlack;
        btnSkills[3].interactable = false;
        btnSkills[3].GetComponent<Image>().color = colorBtnBlack;
    }
    public void EnableBtnSkills()
    {
        btnSkills[0].interactable = true;
        btnSkills[0].GetComponent<SkillButton>().enabled = true;
        btnSkills[0].GetComponent<Image>().color = colorBtnStandard;
        btnSkills[1].interactable = true;
        btnSkills[1].GetComponent<SkillButton2>().enabled = true;
        btnSkills[1].GetComponent<Image>().color = colorBtnStandard;
        btnSkills[2].interactable = true;
        btnSkills[2].GetComponent<SkillButton3>().enabled = true;
        btnSkills[2].GetComponent<Image>().color = colorBtnStandard;
        btnSkills[3].interactable = true;
        btnSkills[3].GetComponent<Image>().color = colorBtnStandard;

    }
    public void DisableBtnMoviment()
    {
        btnMoviment.GetComponent<FixedJoystick>().enabled = false;
        btnMoviment.GetComponent<Image>().color = colorBtnBlack;
        Image hand = btnMoviment.GetComponentsInChildren<Image>()[1];
        hand.color = colorBtnBlack;
             
    }
    public void EnableBtnMoviment()
    {
        btnMoviment.GetComponent<FixedJoystick>().enabled = true;
        btnMoviment.GetComponent<Image>().color = colorBtnStandard;
        btnMoviment.GetComponentInChildren<Image>().color = colorBtnStandard;
        Image hand = btnMoviment.GetComponentsInChildren<Image>()[1];
        hand.color = colorBtnStandard;
    }
    public void DisableBtnAttack()
    {
        btnAttack.interactable = false;
        btnAttack.GetComponent<ButtonDown>().enabled = false;
        btnAttack.GetComponent<Image>().color = colorBtnBlack;
    }
    public void EnableBtnAttack()
    {

        btnAttack.interactable = true;
        btnAttack.GetComponent<ButtonDown>().enabled = true;
        btnAttack.GetComponent<Image>().color = colorBtnStandard;
    }
    public void DisableBtnPotions()
    {
        btnPotions[0].interactable = false;
        btnPotions[0].GetComponent<Image>().color = colorBtnBlack;
        btnPotions[1].interactable = false;
        btnPotions[1].GetComponent<Image>().color = colorBtnBlack;
    }
    public void EnableBtnPotions()
    {
        btnPotions[0].interactable = true;
        btnPotions[0].GetComponent<Image>().color = colorBtnStandard;
        btnPotions[1].interactable = true;
        btnPotions[1].GetComponent<Image>().color = colorBtnStandard;
    }


}
