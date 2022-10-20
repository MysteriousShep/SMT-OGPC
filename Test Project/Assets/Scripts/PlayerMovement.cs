using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 movement;
    public Rigidbody2D rb;
    public BoxCollider2D hitbox;

    private float yVelocity;
    private int coyoteFrame = 0;
    private bool grounded = false;
    private int jumpFrame = 0;
    private int dashFrame = 0;
    
    public float speed = 5.0f;
    public float jumpSpeed = 10.0f;
    public int jumpDuration = 20;
    public float gravity;
    public int coyoteTime = 0;
    public bool hasDash = false;
    public float dashSpeed = 30;
    public int dashLength = 5;
    public float dashCooldown = 1.0f;
    

    // Update is called once per frame
    void Update()
    {
        if (dashFrame <= 0)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
        }
        movement.y = Input.GetAxisRaw("Jump");
    }

    void FixedUpdate()
    {
        // Get grounded
        Vector3 max = hitbox.bounds.max;
        Vector3 min = hitbox.bounds.min;
        Vector2 corner1 = new Vector2(max.x-0.05f,min.y-0.1f);
        Vector2 corner2 = new Vector2(min.x+0.05f,min.y-0.2f);
        Collider2D hit = Physics2D.OverlapArea(corner1,corner2);
        grounded = false;
        if (hit != null) {
            grounded = true;
        }
        // Movement
        yVelocity = rb.velocity.y;
        yVelocity -= gravity*Time.deltaTime;
        if (jumpFrame < jumpDuration && movement.y > 0 && coyoteFrame < coyoteTime) {
            jumpFrame += 1;
            yVelocity = jumpSpeed;
        } 
        else if (jumpFrame < jumpDuration && (jumpFrame > 0 || coyoteFrame >= coyoteTime) && !grounded) {
            jumpFrame += 2;
        } 
        else if (!grounded && coyoteFrame < coyoteTime)
        {
            coyoteFrame += 1;
        }
        if (grounded) {
            jumpFrame = 0;
            coyoteFrame = 0;
        }
        // Dash
        if (dashFrame > dashCooldown*-60.0f)
        {
            dashFrame -= 1;
        }
        float dashInput = Input.GetAxis("Fire1");
        if (hasDash && dashInput > 0 && dashFrame <= dashCooldown*-60.0f && Mathf.Abs(movement.x) > 0.5f) {
            dashFrame = dashLength;
            
        }
        if (dashFrame > 0)
        {
            rb.velocity = new Vector2(movement.x*dashSpeed,yVelocity*0.5f);
            
        }
        else if (dashFrame == 0) 
        {
            rb.velocity = new Vector2(0,0);
        }
        else
        {
            rb.velocity = new Vector2(movement.x*speed,yVelocity);
        }

    }
}
