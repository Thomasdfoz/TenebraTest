using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SkillButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler, IDragHandler
{
    public Image skillAreaEffect;
    public GameObject player;
    public GameObject skillAreaPrefab;
    private bool isMoving;
    private float posY;
    private float posX;
    private float posZ;
    public Joystick joy;
    public RectTransform[] limites;
    // Start is called before the first frame update
    void Start()
    {
        joy = GetComponentInChildren<FixedJoystick>();
        joy.gameObject.SetActive(false);
        posY = -1;
        posX = 0;
        posZ = 0;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Vector3 direction = player.transform.position;
        //float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        if (isMoving)
        {
            posX += (joy.Horizontal / 4);
            posZ += (joy.Vertical / 4);

            if (posX > limites[3].anchoredPosition3D.x)
            {
                posX = limites[3].anchoredPosition3D.x;
            }
            if (posX < limites[1].anchoredPosition3D.x)
            {
                posX = limites[1].anchoredPosition3D.x;
            }
            if (posZ > limites[2].anchoredPosition3D.z)
            {
                posZ = limites[2].anchoredPosition3D.z;
            }
            if (posZ < limites[0].anchoredPosition3D.z)
            {
                posZ = limites[0].anchoredPosition3D.z;

            }




            skillAreaEffect.rectTransform.anchoredPosition3D = new Vector3(posX, posY, posZ);
        }
        Vector3 difference = skillAreaEffect.transform.position - player.transform.position;
        float rotationZ = Mathf.Atan2(difference.z, difference.x) * Mathf.Rad2Deg;
        skillAreaEffect.transform.rotation = Quaternion.Euler(90.0f, 0.0f, rotationZ - 90f);

    }
    public void OnPointerUp(PointerEventData data)
    {
        skillAreaEffect.gameObject.SetActive(false);
        GetComponent<Button>().interactable = true;
        joy.gameObject.SetActive(false);
        GameObject magic = Instantiate(skillAreaPrefab);
        Vector3 pos = new Vector3(skillAreaEffect.rectTransform.position.x, skillAreaPrefab.transform.position.y, skillAreaEffect.rectTransform.position.z);
        magic.transform.position = pos;


        joy.OnPointerUp(data);

        isMoving = false;
        posY = -1;
        posX = 0;
        posZ = 0;

    }
    public void OnPointerDown(PointerEventData data)
    {
        GetComponent<Button>().interactable = false;
        joy.gameObject.SetActive(true);

        isMoving = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        joy.OnDrag(eventData);
        if (joy.Vertical > 0.3f || joy.Horizontal > 0.3f || joy.Vertical < -0.3f || joy.Horizontal < -0.3f)
        {
            skillAreaEffect.gameObject.SetActive(true);
        }
    }

    public void OnPointerClick(PointerEventData data)
    {


    }
}
