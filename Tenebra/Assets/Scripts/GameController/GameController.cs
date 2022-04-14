using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // isso aki vai ficar dentro do scriptable da magia
    public AnimationClip animSpecialAttack;
    public Color color;

    [Header("Main")]
    public GameObject player;
    private PlayerController playerController;
    private PlayerStats playerStats;
    private ButtonsActive buttonsActive;

    public Skills_Scriptable skill;

    public PlayerController PlayerController { get => playerController; set => playerController = value; }
    public PlayerStats PlayerStats { get => playerStats; set => playerStats = value; }
    public ButtonsActive ButtonsActive { get => buttonsActive; set => buttonsActive = value; }

    // Start is called before the first frame update
    void Start()
    {
        PlayerStats = player.GetComponent<PlayerStats>();
        PlayerController = player.GetComponent<PlayerController>();
        ButtonsActive = GetComponent<ButtonsActive>();
    }

    // Update is called once per frame
    void Update()
    {
        //fazer igual o burning damage para dar um burning heal tambem nas potion e magias desse jeito
        if (PlayerStats.Life.CurrentValue <= 0)
        {
            player.gameObject.SetActive(false);
            PlayerStats.IsDead = true;
            Debug.Log("morto");
        }
        #region teste de burning
        //----------------------------teste de burning esse codigo deve ficar dentro da magia ou flecha com o dano burning --------------
        if (Input.GetKeyDown(KeyCode.P))
        {
            BurningManager.InvokeBurning(player, "Fogo", 15, 2, DamageType.magic);
            
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            BurningManager.InvokeBurning(player, "Veneno", 5, 20, DamageType.magic);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            BurningManager.InvokeBurning(player, "Gelo", 10, 4, DamageType.magic);
        }
        #endregion
        //test
        if (Input.GetKeyDown(KeyCode.A))
        {
            ButtonsActive.ActiveAll(true);

        }
        //test
        if (Input.GetKeyDown(KeyCode.B))
        {
            ButtonsActive.ActiveAll(false);
        }
    }

    

}
