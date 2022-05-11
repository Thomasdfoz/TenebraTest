using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cast : MonoBehaviour
{
    private Transform position;
    private GameObject prefabEffect;
    private float moveSpeed;

    public GameObject PrefabEffect { get => prefabEffect; set => prefabEffect = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public Transform Position { get => position; set => position = value; }

    public void Constructor(Transform position, GameObject prefabEffect, float moveSpeed)
    {
        this.position = position;
        this.PrefabEffect = prefabEffect;
        this.MoveSpeed = moveSpeed;
    }
    public void InstantiateCast()
    {
        GameObject Obj = Instantiate(PrefabEffect, position.position, position.rotation);
        if (MoveSpeed > 0)
        {
            Obj.GetComponent<Rigidbody>().velocity = (position.forward * MoveSpeed);
        }
    }
}
