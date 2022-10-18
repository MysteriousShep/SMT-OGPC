using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public Vector2 movement;
    public Rigidbody2D rb;
    
    public int jumpFrame = 0;
    public float jumpSpeed = 10.0f;
    public int jumpDuration = 20;

    private float yVelocity;
    public float gravity;
    private bool grounded = false;
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        yVelocity = rb.velocity.y;
        yVelocity -= gravity*Time.deltaTime;
        if (jumpFrame < jumpDuration && movement.y > 0) {
            jumpFrame += 1;
            yVelocity = jumpSpeed;
        } else if (jumpFrame < jumpDuration && !grounded) {
            jumpFrame += 2;
        }
        if (grounded) {
            jumpFrame = 0;
        }
        rb.velocity = new Vector2(movement.x*speed,yVelocity);

    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (!(other.transform.position.y > transform.position.y))
        {
            grounded = true;
        }
    }
    void OnCollisionStay2D(Collision2D other)
    {
        OnCollisionEnter2D(other);
    }
    void OnCollisionExit2D(Collision2D other)
    {
        grounded = false;
    }
}
