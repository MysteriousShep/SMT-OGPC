using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public List<Vector3> trail;
    public int followDelay = 10;
    public float gravity = 0;
    public bool first = false;
    public float wind = 0;
    void FixedUpdate()
    {
        if (trail.Count >= followDelay)
        {
            if (Vector3.Distance(player.transform.position,trail[followDelay-1]) > 0.0f)
            {
                //Add previous player position to follow list while player is moving
                trail.Add(player.transform.position);
            }
            else
            {
                //Stop moving when player is not moving
            }
        }
        else
        {
            //Set up list during the first frames
            trail.Add(player.transform.position);
        }
        //Set position
        if (first) {
            transform.Translate(Vector3.right*0);
        }
        if (trail.Count > followDelay)
        {
            trail.RemoveAt(0);
            transform.position = trail[0];
        }
        
        transform.Translate(Vector3.left*-wind);
        transform.position = Vector3.MoveTowards(transform.position,player.transform.position,gravity);
        transform.Translate(Vector3.down*gravity);
        transform.Translate(Vector3.left*wind);
        if (first) {
            transform.Translate(Vector3.right*-0);
        }
    }
}
