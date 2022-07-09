using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create Race", menuName = "Race/New Race")]
public class RaceScriptable : ScriptableObject
{
    public RaceEnum race;
    public string textInfoRace;
    public ClassesScriptable[] classes;
}
