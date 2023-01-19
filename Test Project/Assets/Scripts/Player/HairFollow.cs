using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairFollow : MonoBehaviour
{
    public GameObject player;
    public List<Vector3> trail;
    public int followDelay = 10;
    public float gravity = 0;
    public bool first = false;
    public float wind = 0;
    public int index = 2;

    void Start()
    {
        for (int i = 0; i < followDelay; i++)
        {
            trail.Add(player.transform.position);
        }
    }

    void FixedUpdate()
    {
        if (trail.Count >= followDelay)
        {
            //Add previous player position to follow list
            trail.Add(player.transform.position);
        }
        //Set position
        if (trail.Count > followDelay)
        {
            trail.RemoveAt(0);
            transform.position = trail[0];
            transform.Translate(Vector3.back*-1f);
        }
        
        transform.Translate(Vector3.left*-wind*gravity);
        Vector3 targetPos = player.transform.position;
        targetPos.z += 1f;
        transform.position = Vector3.MoveTowards(transform.position,targetPos,gravity);
        transform.Translate(Vector3.down*gravity);
        transform.Translate(Vector3.left*wind);
        transform.Translate(Vector3.up*Mathf.Sin((Time.time))*index*wind*0.25f);
        
    }
}
