using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SkillButton2 : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler, IDragHandler
{
    public Image skillArrowEffect;
    public GameObject spanwPoint;
    public GameObject skillProjectilePrefab;
    public Joystick joy;
    // Start is called before the first frame update
    void Start()
    {
        joy = GetComponentInChildren<FixedJoystick>();
        joy.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = new Vector3(joy.Horizontal, 0f, joy.Vertical).normalized;
        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
            skillArrowEffect.rectTransform.rotation = Quaternion.Euler(90, 0, targetAngle);
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            spanwPoint.transform.rotation = Quaternion.Euler(0, targetAngle, 0);
        }
    }
    public void OnPointerUp(PointerEventData data)
    {
        skillArrowEffect.gameObject.SetActive(false);
        GetComponent<Button>().interactable = true;
        joy.gameObject.SetActive(false);
        joy.OnPointerUp(data);
        GameObject missile = Instantiate(skillProjectilePrefab, spanwPoint.transform.position, spanwPoint.transform.rotation);

        missile.GetComponent<Rigidbody>().velocity = (spanwPoint.transform.forward * 3);
        

    }
    public void OnPointerDown(PointerEventData data)
    {
        GetComponent<Button>().interactable = false;
        joy.gameObject.SetActive(true);    
    }

    public void OnDrag(PointerEventData eventData)
    {
        joy.OnDrag(eventData);
        if (joy.Vertical > 0.3f || joy.Horizontal > 0.3f || joy.Vertical < -0.3f || joy.Horizontal < -0.3f)
        {
            skillArrowEffect.gameObject.SetActive(true);
        }
    }

    public void OnPointerClick(PointerEventData data)
    {


    }
}
