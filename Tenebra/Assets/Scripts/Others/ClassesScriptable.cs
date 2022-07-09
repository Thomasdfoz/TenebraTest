using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create Classe", menuName = "Classe/New Classe")]
public class ClassesScriptable : ScriptableObject
{
    public ClasseEnum classe;
    public Vector3 skillExitPoint;
    public string textInfoClasse;
    public GameObject prefab;
}

