using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCharGame : MonoBehaviour
{
    public ClassesScriptable charClasse; 
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public void StartGamePlayer()
    {
        SceneManager.LoadScene("Game");
    }
}
