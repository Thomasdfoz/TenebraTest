using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    [Header("models")]
    [SerializeField] GameObject[] models;
    [Header("Races")]
    [SerializeField] RaceScriptable[] races;
    [Space(40)]
    [SerializeField] GameObject painelMain;
    [SerializeField] GameObject painelSelect;
    [SerializeField] GameObject painelCreate;
    [SerializeField] GameObject painelCredits;
    [SerializeField] Animator anime;
    [SerializeField] TMP_Text RaceInfoTitle;
    [SerializeField] TMP_Text RaceInfoText;
    [SerializeField] TMP_Text ClasseInfoTitle;
    [SerializeField] TMP_Text ClasseInfoText;
    [Header("Sliders and Texts")]
    [SerializeField] Slider ClasseSlider;
    [SerializeField] Slider RaceSlider;
    [SerializeField] TMP_Text raceText;
    [SerializeField] TMP_Text classeText;

    [Header("Buttons")]
    [SerializeField] GameObject createBtn;
    [SerializeField] GameObject selectBtn;
    [Header("GameManager")]
    [SerializeField] LoadCharGame loadCharGame;
    int idRace;
    int idClasse;

    int idClasseName;

    bool isPlay;
    private void Awake()
    {
        idRace = 0;
        idClasse = 0;
        idClasseName = 0;
    }
    void Start()
    {
        ActivePainel(painelMain);
        isPlay = false;
        UpdateNames(idClasse);
        CleanSliders();
        GetInfoText(idRace);
    }

    void Update()
    {

        if (idRace != RaceSlider.value)
        {
            idRace = Mathf.FloorToInt(RaceSlider.value);
            GetInfoText(idRace);
            TradeModel();
        }
        if (idClasse != ClasseSlider.value)
        {
            idClasse = Mathf.FloorToInt(ClasseSlider.value);
            UpdateNames(idClasse);
            GetInfoText(idRace);
            TradeModel();
        }
    }
    void UpdateNames(int i)
    {
        if (i < 4)
        {
            idClasseName = i;
        }
        else if (i < 8)
        {
            idClasseName = i - 4;
        }
        else if (i < 12)
        {
            idClasseName = i - 8;
        }
        else if (i < 16)
        {
            idClasseName = i - 12;
        }
        raceText.text = races[idRace].race.ToString();
        classeText.text = races[idRace].classes[idClasseName].classe.ToString();

    }
    private void GetInfoText(int idRace)
    {
        RaceInfoTitle.text = races[idRace].race.ToString();
        RaceInfoText.text = races[idRace].textInfoRace;
        ClasseInfoTitle.text = races[idRace].classes[idClasseName].classe.ToString();
    }
        private void TradeModel()
    {
        foreach (var item in models)
        {
            item.SetActive(false);
        }
        models[idClasse].SetActive(true);
    }

    public void ChangeSlider()
    {
        switch (RaceSlider.value)
        {
            case 0:
                ClasseSlider.minValue = 0;
                ClasseSlider.maxValue = 3;
                break;
            case 1:
                ClasseSlider.minValue = 4;
                ClasseSlider.maxValue = 7;
                break;
            case 2:
                ClasseSlider.minValue = 8;
                ClasseSlider.maxValue = 11;
                break;
            case 3:
                ClasseSlider.minValue = 12;
                ClasseSlider.maxValue = 15;
                break;
            default:
                break;
        }
        ClasseSlider.value = ClasseSlider.minValue;
    }

    private void CleanSliders()
    {
        RaceSlider.value = 0;
        ClasseSlider.value = 0;
    }


    public void LoadCreateChar()
    {
        CleanSliders();
        ActivePainel(painelCreate);

    }

    public void PlayGame()
    {
        loadCharGame.charClasse = races[idRace].classes[idClasseName];
        loadCharGame.StartGamePlayer();

    }

    public void LoadCredits()
    {
        ActivePainel(painelCredits);
    }
    public void LoadMain()
    {
        ActivePainel(painelMain);
        if (isPlay)
        {
            anime.SetTrigger("Return");
            isPlay = false;
        }

    }
    public void LoadSelectChar()
    {
        TradeModel();
        ActivePainel(painelSelect);
        if (!isPlay)
        {
            anime.SetTrigger("Play");
            isPlay = true;
        }
        createBtn.SetActive(true);
        selectBtn.SetActive(false);
    }
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void ActivePainel(GameObject painel)
    {
        painelMain.SetActive(false);
        painelCreate.SetActive(false);
        painelCredits.SetActive(false);
        painelSelect.SetActive(false);
        painel.SetActive(true);
    }
}

