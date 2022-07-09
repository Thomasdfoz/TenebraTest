using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerBody;
    [SerializeField] private TextMeshProUGUI infoText;
    private SavedAndLoad save;
    private PlayerController playerController;
    private PlayerStats playerStats;
    private ButtonsAndTextManager buttonsActive;
    private LoadCharGame playerLoad;
    private ClassesScriptable classe;

    public Skills_Scriptable[] skill;

    public PlayerController PlayerController { get => playerController; set => playerController = value; }
    public PlayerStats PlayerStats { get => playerStats; set => playerStats = value; }
    public ButtonsAndTextManager ButtonsActive { get => buttonsActive; set => buttonsActive = value; }
    public TextMeshProUGUI InfoText { get => infoText; set => infoText = value; }
    public GameObject Player { get => player; set => player = value; }

    void Awake()
    {
        PlayerStats = Player.GetComponent<PlayerStats>();
        PlayerController = Player.GetComponent<PlayerController>();
        ButtonsActive = GetComponent<ButtonsAndTextManager>();
        save = GetComponent<SavedAndLoad>();
        playerLoad = FindObjectOfType<LoadCharGame>();
            
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartChar());
    }
    IEnumerator StartChar()
    {
        yield return new WaitForEndOfFrame();
        classe = playerLoad.charClasse;
        Instantiate(classe.prefab, playerBody.transform);
    }
    // Update is called once per frame
    void Update()
    {
        //fazer igual o burning damage para dar um burning heal tambem nas potion e magias desse jeito
        if (PlayerStats.Life.CurrentValue <= 0)
        {
            Player.gameObject.SetActive(false);
            PlayerStats.IsDead = true;
            ButtonsActive.ActiveAll(false);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            save.SaveGame();
        }
        #region teste de burning
        //----------------------------teste de burning esse codigo deve ficar dentro da magia ou flecha com o dano burning --------------
        if (Input.GetKeyDown(KeyCode.P))
        {
            BurningManager.InvokeBurning(Player, "Fogo", 15, 2, DamageType.magic);
            
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            BurningManager.InvokeBurning(Player, "Veneno", 5, 20, DamageType.magic);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            BurningManager.InvokeBurning(Player, "Gelo", 10, 4, DamageType.magic);
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

    public void SetTextInfo(string text)
    {
        StopAllCoroutines();
        infoText.text = text;
        StartCoroutine(DeleteText());
    }

    private IEnumerator DeleteText()
    {
        yield return new WaitForSeconds(5);
        SetTextInfo(" ");
    }

    

}
