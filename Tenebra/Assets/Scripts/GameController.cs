using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // isso aki vai ficar dentro do scriptable da magia
    public AnimationClip animSpecialAttack;


    [Header("Main")]
    public GameObject Player;
    PlayerController playerController;
    PlayerStats playerStats;
    public ButtonsActive buttonsActive;

    public SkillsScriptable skill;



    

    // Start is called before the first frame update
    void Start()
    {
        playerStats = Player.GetComponent<PlayerStats>();
        playerController = Player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStats.Life.CurrentValue <= 0)
        {
            Player.gameObject.SetActive(false);
            playerStats.IsDead = true;
            Debug.Log("morto");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            playerStats.TookDamage(new SendDamage(20, 30, DamageType.physical));
        }
        
        //test
        if (Input.GetKeyDown(KeyCode.A))
        {
            buttonsActive.ActiveAll(true);
            
        }
        //test
        if (Input.GetKeyDown(KeyCode.B))
        {
            buttonsActive.ActiveAll(false);
        }
    }
    

}
