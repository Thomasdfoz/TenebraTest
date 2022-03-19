using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonDown : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler, IDragHandler
{
    public GameObject arcoRanged;
    public Joystick joy;
    public PlayerMoviment player;
    public Image mira;
    public Transform[] limites;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerUp(PointerEventData data)
    {
        arcoRanged.SetActive(false);
        GetComponent<Button>().interactable = true;
        joy.gameObject.SetActive(false);
        player.miraBolean = false;
        mira.gameObject.SetActive(false);
        mira.rectTransform.localPosition = new Vector3(0, 0, 0);
        joy.OnPointerUp(data);
        if (player.selectedTarget)
        {
            player.AttackSelected();
        }
    }
    public void OnPointerDown(PointerEventData data)
    {
        
        GetComponent<Button>().interactable = false;
        joy.gameObject.SetActive(true);
        player.miraBolean = true;
        player.RangedSelectTarget();
    }

    public void OnDrag(PointerEventData eventData)
    {
        joy.OnDrag(eventData);
        if (joy.Vertical > 0.3f || joy.Horizontal > 0.3f || joy.Vertical < -0.3f || joy.Horizontal < -0.3f)
        {

            arcoRanged.SetActive(true);
            mira.gameObject.SetActive(true);

        }
    }

    public void OnPointerClick(PointerEventData data)
    {


    }
}
