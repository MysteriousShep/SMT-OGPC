using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossesedTrail : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        player = GameObject.FindWithTag("Possesed Player");
        if (player != null)
        {
            transform.position = new Vector3(player.transform.position.x,player.transform.position.y,1f);
        }
    }
}
