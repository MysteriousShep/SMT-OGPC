using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 movement = new Vector2(0,0); // Current input
    public float speed = 5.0f; // How fast the player should move
    public List<GameObject> hair;
    public GameObject hairObject;
    public int hairLength = 6;
    public Sprite hairSprite;

    void Start() 
    {
        hair.Add(Instantiate(hairObject,transform.position,transform.rotation));
        hair[0].GetComponent<FollowPlayer>().player = gameObject;
        hair[0].GetComponent<FollowPlayer>().first = true;
        for (int i = 1; i < hairLength; i++)
        {
            hair.Add(Instantiate(hairObject,transform.position,transform.rotation));
            hair[i].GetComponent<FollowPlayer>().player = hair[i-1];
            if (i > hairLength-4)
            {
                hair[i].GetComponent<SpriteRenderer>().sprite = hairSprite;
            }
        }
    }

    // Every frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); // Set movement X to HORIZONTAL input
        movement.y = Input.GetAxisRaw("Vertical"); // Set movement Y to VERTICAL input
        if (movement.x < 0)
        {
            for (int i = 0; i < hairLength; i++)
            {
                hair[i].transform.localScale = new Vector3(-0.5f,0.5f,1);
            }
            
        }
        if (movement.x > 0)
        {
            for (int i = 0; i < hairLength; i++)
            {
                hair[i].transform.localScale = new Vector3(0.5f,0.5f,1);
            }
        }
    }

    // Every 60th of a second
    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = movement*speed; // Move player based on input
    }
}
