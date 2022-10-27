using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosses : MonoBehaviour
{
    public Vector3 mouse;
    public bool possesed = false;

    // Update is called once per frame
    void Update()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;
        if (Input.GetButtonDown("Fire1"))
        {
            if (Vector3.Distance(transform.position,mouse)<1f)
            {
                possesed = true;
                gameObject.tag = "Possesed Player";
            }
            else
            {
                possesed = false;
                gameObject.tag = "Player";
            }
        }
    }
}
