using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        if (Input.GetAxisRaw("Fire1")>0.0f && Vector3.Distance(transform.position,mousePos) < 0.5f)
        {
            transform.position = mousePos;
        }

    }
}
