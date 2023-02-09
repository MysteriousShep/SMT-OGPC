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
    public Sprite sprite;
    public Sprite hairSprite;

    void Start() 
    {
        hair.Add(Instantiate(hairObject,new Vector3(transform.position.x,transform.position.y-0.1f,transform.position.z-1),transform.rotation));
        hair[0].GetComponent<HairFollow>().player = gameObject;
        hair[0].GetComponent<HairFollow>().first = true;
        for (int i = 1; i < hairLength; i++)
        {
            hair.Add(Instantiate(hairObject,new Vector3(transform.position.x,transform.position.y-i*0.1f,transform.position.z-1),transform.rotation));
            hair[i].GetComponent<HairFollow>().player = hair[i-1];
            hair[i].GetComponent<HairFollow>().index = i/4+2;
            if (i > hairLength-3)
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
            transform.localScale = new Vector3(-0.75f,0.75f,1);
        }
        if (movement.x > 0)
        {
            for (int i = 0; i < hairLength; i++)
            {
                hair[i].transform.localScale = new Vector3(0.5f,0.5f,1);
            }
            transform.localScale = new Vector3(0.75f,0.75f,1);
        }
    }

    // Every 60th of a second
    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = movement*speed; // Move player based on input
    }
}
