using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quality : MonoBehaviour
{
    public int qualityindex = 0;
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.SetQualityLevel(qualityindex);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            qualityindex++;
            if (qualityindex >= 5)
            {
                qualityindex = 5;
            }
            QualitySettings.SetQualityLevel(qualityindex);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            qualityindex--;
            if (qualityindex <= 0)
            {
                qualityindex = 0;
            }
            QualitySettings.SetQualityLevel(qualityindex);
        }

    }
}
