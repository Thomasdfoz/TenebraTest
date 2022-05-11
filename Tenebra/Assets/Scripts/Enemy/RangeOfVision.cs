using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeOfVision : MonoBehaviour
{
    private GameObject target;

    public GameObject CheckLook()
    {
        return target;
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            if (!target)
            {
                target = col.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            if (col.gameObject == target)
            {
                target = null;
            }
        }
    }
}
