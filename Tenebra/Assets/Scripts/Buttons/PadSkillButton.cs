using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PadSkillButton : MonoBehaviour
{
    [Header("Effects")]
    public Image areaEffect;
    public Image projectileEffect;
    public Transform lookObj;
    public RawImage mira;
    [Header("GameObjs")]
    public GameObject spanwPoint;
    public GameObject cicleRanged;
    [Header("Limites")]
    public RectTransform[] limites;

    void Start()
    {
       
        StartCoroutine(FindPoints());
    }

    private IEnumerator FindPoints()
    {
        yield return new WaitForEndOfFrame();
        spanwPoint = GameObject.Find("SpawnPoint");
        if (spanwPoint == null)
        {
            StartCoroutine(FindPoints());
        }
    }

}
