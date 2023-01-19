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
    public bool hasDash;
    public Sprite dashSprite;
    public int dashFrame = 0;
    public int dashCooldown = 20;
    public float dashSpeed = 10;

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
        if (dashFrame <= 0)
        {
            
            movement.x = Input.GetAxisRaw("Horizontal"); // Set movement X to HORIZONTAL input
            movement.y = Input.GetAxisRaw("Vertical"); // Set movement Y to VERTICAL input
            if (Input.GetButtonDown("Jump") && hasDash)
            {
                dashFrame = dashCooldown;
                if (movement.x == 0 && movement.y == 0) 
                {
                    movement.x = Mathf.Sign(transform.localScale.x);
                }
            }
            for (int i = 0; i < hairLength; i++)
            {
                hair[i].GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
            }
            GetComponent<SpriteRenderer>().sprite = sprite;
        }
        else
        {
            dashFrame -= 1;
            hair[0].GetComponent<SpriteRenderer>().color = new Color(1,0,0,1);
            
            hair[0].GetComponent<HairFollow>().trail[1] = transform.position;
            hair[0].transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z+1);
            hair[0].transform.Translate(movement*dashSpeed*Time.deltaTime*1/3);
            if (dashFrame <= 0)
            {
                hair[0].GetComponent<HairFollow>().trail.RemoveAt(0);
                hair[0].GetComponent<HairFollow>().trail.RemoveAt(0);
                hair[0].transform.position = new Vector3(transform.position.x,transform.position.y-0.1f,transform.position.z+1);
                for (int j = 0; j < hair[0].GetComponent<HairFollow>().followDelay; j++)
                {
                    hair[0].GetComponent<HairFollow>().trail.Add(transform.position);
                }
            }
            
            for (int i = 1; i < hairLength; i++)
            {
                if (dashFrame > -5) {
                    hair[i].GetComponent<SpriteRenderer>().color = new Color(1,0,0,1);
                    
                    hair[i].GetComponent<HairFollow>().trail[1] = hair[i].transform.position;
                    hair[i].transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z+1);
                    hair[i].transform.Translate(movement*dashSpeed*-Time.deltaTime*i/4);
                    
                    hair[i].GetComponent<HairFollow>().trail[0] = hair[i].transform.position;
                    hair[i].GetComponent<HairFollow>().trail[1] = hair[i].transform.position;
                    //hair[i].GetComponent<HairFollow>().trail[2] = hair[i-1].transform.position;
                }
                else
                {
                    /*
                    hair[i].GetComponent<HairFollow>().trail.RemoveAt(0);
                    hair[i].GetComponent<HairFollow>().trail.RemoveAt(0);
                    hair[i].GetComponent<HairFollow>().trail.RemoveAt(0);
                    
                    hair[i].transform.position = new Vector3(transform.position.x,transform.position.y-0.1f*i,transform.position.z);
                    for (int j = 0; j < hair[i].GetComponent<HairFollow>().followDelay; j++)
                    {
                        hair[i].GetComponent<HairFollow>().trail.Add(hair[i].GetComponent<HairFollow>().player.transform.position);
                    }
                    */
                }
                
            }
            GetComponent<SpriteRenderer>().sprite = dashSprite;
        }
            
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
        if (dashFrame <= 0)
        {
            GetComponent<Rigidbody2D>().velocity = movement*speed; // Move player based on input
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = movement*dashSpeed; // Move player based on input
        }
    }
}
