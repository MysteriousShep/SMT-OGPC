using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject player;
    public float smoothing = 1.0f;
    public float speed = 0.25f;
    private Vector2 movement;

    // Update is called once per frame
    void LateUpdate()
    {
        player = GameObject.FindWithTag("Possesed Player");
        if (player != null)
        {
            Vector3 targetPosition = player.transform.position;
            targetPosition = new Vector3(targetPosition.x,targetPosition.y,transform.position.z);
            transform.position = Vector3.Lerp(transform.position,targetPosition,smoothing);
        }
        else
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            transform.position = new Vector3(transform.position.x+movement.x*speed*Time.deltaTime,transform.position.y+movement.y*speed*Time.deltaTime,transform.position.z);
        }
    }
}
