using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private bool selected = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        if (Input.GetButtonDown("Fire1"))
        {
            selected = Vector3.Distance(transform.position,mousePos) < 0.5f;
        }
        if (Input.GetButtonUp("Fire1"))
        {
            selected = false;
        }
        if (selected)
        {
            transform.position = mousePos;
        }
    }
}
