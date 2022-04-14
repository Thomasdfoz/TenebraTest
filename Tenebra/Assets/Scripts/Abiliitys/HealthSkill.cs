using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSkill : MonoBehaviour
{

    public float time;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Destroy", time);
    }

    IEnumerator Destroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
