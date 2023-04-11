using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshake : MonoBehaviour
{
    public float screenshake = 0;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = player.transform.position + new Vector3(0, 1, -10);
        int xRandom = Random.Range(0, 2);
        int yRandom = Random.Range(0, 2);
        if (xRandom == 1)
        {
            transform.position = new Vector3(transform.position.x + screenshake * 0.1f, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x - screenshake * 0.1f, transform.position.y, transform.position.z);
        }
        if (yRandom == 1)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + screenshake * 0.1f, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - screenshake * 0.1f, transform.position.z);
        }
        screenshake = Mathf.Max(0,screenshake-1);
    }
}
