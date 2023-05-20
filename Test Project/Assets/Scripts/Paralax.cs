using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    public GameObject player;
    public float distance = 10f;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position / distance;
        transform.position = new Vector3(transform.position.x, transform.position.y, distance / 100);
    }
}
