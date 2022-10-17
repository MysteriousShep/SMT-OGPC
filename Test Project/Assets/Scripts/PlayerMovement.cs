using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public Vector2 movement;
    public Rigidbody2D rb;
    private float jumpFrame = 0.0f;
    private float yVelocity;
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        yVelocity = rb.velocity.y;
        if (jumpFrame < 10 && movement.y > 0) {
            jumpFrame += 1;
            yVelocity = 10.0f;
        } else if (jumpFrame < 10 && jumpFrame > 0) {
            jumpFrame -= 1;
        }
        rb.velocity = new Vector2(movement.x*speed,yVelocity);

    }
    void OnTriggerEnter(Collider other)
    {
        jumpFrame = 10.0f;
    }
}
