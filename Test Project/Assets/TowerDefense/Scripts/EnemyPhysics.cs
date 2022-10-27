using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhysics : MonoBehaviour
{
    public Vector2 movement;
    public Rigidbody2D rb;
    public BoxCollider2D hitbox;

    public float yVelocity = 0.0f;
    public float xVelocity = 0.0f;
    private bool grounded = false;
    public int currentTarget = 0;
    public Vector3[] path;
    public int nextJumpPoint = 0;
    public Vector3[] jumpPoints;

    
    public float speed = 5.0f;
    public float jumpSpeed = 10.0f;
    public int jumpDelay = 30;
    public int jumpFrame = 30;
    public float gravity;

    // Update is called once per frame
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
        if (transform.position.x > path[currentTarget].x)
        {
            xVelocity = -speed;
        }
        else
        {
            xVelocity = speed;
        }
        if (jumpFrame < jumpDelay)
        {
            if (grounded)
            {
                xVelocity = 0;
                jumpFrame -= 1;
                //transform.localScale = new Vector3(1,((0f-jumpFrame*2f)/jumpDelay),1);
                if (jumpFrame < 0)
                {
                    yVelocity = jumpSpeed;
                    jumpFrame = jumpDelay;
                    // transform.localScale = new Vector3(1,1,1);
                }
            }
        }
        if (Vector3.Distance(transform.position,path[currentTarget]) < 1f)
        {
            currentTarget += 1;
            if (currentTarget >= path.Length)
            {
                Destroy(gameObject);
            }
        }
        if (nextJumpPoint < jumpPoints.Length)
        {
            if (Vector3.Distance(transform.position,jumpPoints[nextJumpPoint]) < 1f)
            {
                jumpFrame -= 1;
                nextJumpPoint += 1;
            }
        }
        rb.velocity = new Vector2(xVelocity,yVelocity);
    }
}
