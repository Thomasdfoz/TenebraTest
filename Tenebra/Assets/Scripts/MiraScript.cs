using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiraScript : MonoBehaviour
{
    public GameObject selectableObject;
    public Material materialCuboSelect;
    public Material materialOriginal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Object"))
        {
            selectableObject = other.gameObject;
            if (materialOriginal == null)
            {
                materialOriginal = selectableObject.GetComponent<MeshRenderer>().material;
                selectableObject.GetComponent<MeshRenderer>().material = materialCuboSelect;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Object"))
        {
            selectableObject.GetComponent<MeshRenderer>().material = materialOriginal;
            selectableObject = null;
            materialOriginal = null;
        }
    }
   
}
