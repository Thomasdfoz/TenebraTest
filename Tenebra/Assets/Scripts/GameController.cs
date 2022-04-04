using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // isso aki vai ficar dentro do scriptable da magia
    public AnimationClip animSpecialAttack;


    [Header("Main")]
    public GameObject player;
    public PlayerController playerController;
    public PlayerStats playerStats;
    public ButtonsActive buttonsActive;

    public SkillsScriptable skill;



    

    // Start is called before the first frame update
    void Start()
    {
        playerStats = player.GetComponent<PlayerStats>();
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStats.Life.CurrentValue <= 0)
        {
            player.gameObject.SetActive(false);
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
