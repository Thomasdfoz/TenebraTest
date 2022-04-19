using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsBar : MonoBehaviour
{
    public Image LifeFilled;
    public Image ManaFilled;
    public GameObject controller;
    public bool isPlayer;

    private float currentLife;
    private float maxLife;
    private float currentMana;
    private float maxMana;
      
    void FixedUpdate()
    {
        if (isPlayer)
        {
            currentLife = controller.GetComponent<PlayerStats>().Life.CurrentValue;
            maxLife = controller.GetComponent<PlayerStats>().Life.MaxValue;
            currentMana = controller.GetComponent<PlayerStats>().Mana.CurrentValue;
            maxMana = controller.GetComponent<PlayerStats>().Mana.MaxValue;
            ManaFilled.fillAmount = currentMana / maxMana;
        }
        else
        {
            currentLife = controller.GetComponent<EnemyController>().CurrentLife;
            maxLife = controller.GetComponent<EnemyController>().MaxLife;
        }
        LifeFilled.fillAmount = currentLife / maxLife;
    }
    private void Update()
    {
        gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
