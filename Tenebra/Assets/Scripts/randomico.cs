using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomico : MonoBehaviour
{
    public int num;
    public bool temp;
    // Start is called before the first frame update
    void Start()
    {
        num = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!temp)
        {
            if (num != 34)
            {
                num = Random.Range(0, 1001);
                Debug.Log(num + " false");

            }
            else
            {
                Debug.LogError(num + " true");
                temp = true;
            }

        }
       

    }
}
