using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatTextManager : MonoBehaviour
{

    private static CombatTextManager instance;

    public GameObject textPrefab;
    [Tooltip("0 - AttackText | 1 - CriticText | 2 - MyCriticText | 3 - MyAttackText | 4 - MissText | 5 - ExpText | 6 - HealText")]
    public Color[] textColors;
    [Tooltip("0 - AttackText | 1 - CriticText | 2 - MyCriticText | 3 - MyAttackText | 4 - MissText | 5 - ExpText | 6 - HealText")]
    public int[] FontSize;
    public RectTransform CanvasTransform;

    public float fadeTime;

    public float speed;

    public Vector3 direction;

    public static CombatTextManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CombatTextManager>();
            }

            return instance;
        }

    }
    public void CreatText(Vector3 position, string text, Color color, FontStyle font, int size)
    {
        GameObject sct = Instantiate(textPrefab, position, Quaternion.identity);
        sct.GetComponent<Text>().text = text;
        sct.GetComponent<Text>().color = color;
        sct.GetComponent<Text>().fontStyle = font;
        sct.GetComponent<Text>().fontSize = size;
        sct.transform.SetParent(CanvasTransform);
        sct.GetComponent<RectTransform>().localScale = new Vector3(0.03f, 0.03f, 0.03f);
        sct.GetComponent<RectTransform>().transform.eulerAngles = new Vector3(45, 0, 0);
        sct.GetComponent<RectTransform>().localPosition += new Vector3(0, 4, 0);
        sct.GetComponent<CombatText>().Initialize(speed, direction, fadeTime);

    }
    public void AttackText(Transform pos, int dano)
    {
        Instance.CreatText(pos.position, dano.ToString(), textColors[0],FontStyle.Bold, FontSize[0]);
    }
    public void CriticText(Transform pos, int dano)
    {
        Instance.CreatText(pos.position, "*" + dano.ToString() + "*", textColors[1], FontStyle.Bold, FontSize[1]);
    }
    public void MyCriticText(Transform pos, int dano)
    {
        Instance.CreatText(pos.position, "*" + dano.ToString() + "*", textColors[2], FontStyle.Bold, FontSize[2]);
    }
    public void MyAttackText(Transform pos, int dano)
    {
        Instance.CreatText(pos.position, dano.ToString(), textColors[3], FontStyle.Bold, FontSize[3]);
    }
    public void MissText(Transform pos)
    {
        Instance.CreatText(pos.position, "Miss", textColors[4], FontStyle.Normal, FontSize[4]);
    }
    public void ExpText(Transform pos, int experiencie)
    {
        Instance.CreatText(pos.position, "+" + experiencie.ToString() + " Experience", textColors[5], FontStyle.Bold, FontSize[5]);
    }
    public void HealText(Transform pos, int heal)
    {
        Instance.CreatText(pos.position, heal.ToString() , textColors[6], FontStyle.Bold, FontSize[6]);
    }
}