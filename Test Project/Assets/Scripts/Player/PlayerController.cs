using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 movement = new Vector2(0,0); // Current input
    public float speed = 5.0f; // How fast the player should move
    
    public float turnSpeed = 20.0f; // DELETE ONCE WE HAVE PLAYER SPRITE

    // Every frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); // Set movement X to HORIZONTAL input
        movement.y = Input.GetAxisRaw("Vertical"); // Set movement Y to VERTICAL input
    }

    // Every 60th of a second
    void FixedUpdate()
    {
        
        float angle = Vector2.SignedAngle(Vector2.right,movement); // DELETE WHEN PLAYER SPRITE
        
        Vector3 targetRotation = new Vector3(0, 0, angle); // DELETE WHEN PLAYER SPRITE
        if (GetComponent<Rigidbody2D>().velocity == new Vector2(0,0))
        { // DELETE WHEN PLAYER SPRITE
            angle = Vector2.SignedAngle(Vector2.right,movement); // DELETE WHEN PLAYER SPRITE

            targetRotation = new Vector3(0, 0, angle); // DELETE WHEN PLAYER SPRITE
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation),360); // DELETE WHEN PLAYER SPRITE
        }
        
        GetComponent<Rigidbody2D>().velocity = movement*speed; // Move player based on input

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), turnSpeed); // DELETE WHEN PLAYER SPRITE
        if (movement == new Vector2(0,0))
        { // DELETE WHEN PLAYER SPRITE
            transform.localRotation = new Quaternion(0,0,0,0); // DELETE WHEN PLAYER SPRITE
        }
    }
}
