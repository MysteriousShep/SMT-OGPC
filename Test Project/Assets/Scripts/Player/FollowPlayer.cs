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
        /*if (first) {
            transform.Translate(Vector3.right*-0.25f);
        }*/
        if (trail.Count > followDelay)
        {
            trail.RemoveAt(0);
            transform.position = trail[0];
        }
        transform.position = Vector3.MoveTowards(transform.position,player.transform.position,gravity);
        transform.Translate(Vector3.down*gravity);
        if (first) {
            transform.Translate(Vector3.right*0.25f);
        }
    }
}
