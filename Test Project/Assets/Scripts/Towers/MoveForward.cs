using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 5;

    void FixedUpdate()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}