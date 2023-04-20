using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public Vector2 movement;
    public Animator playerAnimator;
    public float yVelocity;
    private int coyoteFrame = 0;
    private bool grounded = false;
    private int jumpFrame = 0;
    public float jumpSpeed = 10.0f;
    public int jumpDuration = 20;
    public float gravity;
    public int coyoteTime = 0;
    public int accelleration = 3;
    public int deccelleration = 3;
    public float xVelocity = 0;
    public float xSpeed = 0;
    public List<GameObject> hair;
    public GameObject hairObject;
    public int hairLength = 6;
    public Sprite hairSprite;
    public int wallJumpCooldown = 20;
    private int wallJumpFrame = 0;
    private float yMin = 0.25f;
    private float yMax = 1;
    public float jumpSpeedMultiplier = 1.75f;

    // Start is called before the first frame update
    void Start()
    {
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

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Jump");
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
    
    void FixedUpdate()
    {


        

        

        grounded = false;
        if (TouchingGroundAtPlace(transform.position.x, transform.position.y + yVelocity, 1.3f, 0.5f)) 
        {


            TouchingGroundAtPlace(transform.position.x,transform.position.y,1.3f,0.5f);
            for (int i = 0; i < 30; i++)
            {
                if (!TouchingGroundAtPlace(transform.position.x, transform.position.y, 1.3f, 0.5f)) 
                {
                    transform.Translate(new Vector3(0,0.01f*Mathf.Sign(yVelocity),0),Space.World);
                    
                }
            }
            yVelocity = 0;
            
            if (TouchingGroundAtPlace(transform.position.x, transform.position.y, 1.4f, 0.4f))
            {
                grounded = true;
            }
            else
            {
                jumpFrame = jumpDuration+1;
                coyoteFrame = coyoteTime+1;
            }
        }
        
        yVelocity -= gravity*Time.fixedDeltaTime;
        if (grounded) 
        {
            jumpFrame = 0;
            coyoteFrame = 0;
            yVelocity = 0;
            
                if (xSpeed != 0)
                {
                    playerAnimator.SetTrigger("Run");
                    
                }
                else
                {
                    playerAnimator.SetTrigger("Idle");
                }
        }
        else
        {
            if (yVelocity < -0.05f)
            {
                playerAnimator.SetTrigger("Jump");
            }
            else if (yVelocity > 0.05f)
            {
                playerAnimator.SetTrigger("Jump");
            }
            else
            {
                playerAnimator.SetTrigger("Jump");
            }
        }
        if (jumpFrame < jumpDuration && movement.y > 0 && coyoteFrame < coyoteTime) 
        {
            jumpFrame += 1;
            yVelocity = jumpSpeed;
            playerAnimator.SetTrigger("Jump");
            if (jumpFrame < jumpDuration && wallJumpFrame <= 0)
            {
                xVelocity -= xSpeed;
                if (movement.x != 0)
                {
                    xSpeed = jumpSpeedMultiplier * speed * Mathf.Sign(movement.x);
                }
                else
                {
                    if (GetComponent<SpriteRenderer>().flipX)
                    {
                        xSpeed = -jumpSpeedMultiplier * speed;
                    }
                    else
                    {
                        xSpeed = jumpSpeedMultiplier * speed;
                    }
                    
                }
                xVelocity += xSpeed;
            }
            grounded = false;
            
        } 
        else if (jumpFrame < jumpDuration && (jumpFrame > 0 || coyoteFrame >= coyoteTime) && !grounded) 
        {
            jumpFrame += 2;
        }
        else if (!grounded && coyoteFrame < coyoteTime)
        {
            coyoteFrame += 1;
        }
        transform.Translate(new Vector3(0,yVelocity,0),Space.World);
        
        xVelocity -= xSpeed;
        if (wallJumpFrame <= 0) {
            if (movement.x != 0)
            {
                if (movement.x < 0)
                {
                    if (xSpeed > 0)
                    {
                        xSpeed -= speed/accelleration;
                    }
                    if (xSpeed > -speed)
                    {
                        xSpeed -= speed/accelleration;
                    }
                }
                else
                {
                    if (xSpeed < 0)
                    {
                        xSpeed += speed/accelleration;
                    }
                    if (xSpeed < speed)
                    {
                        xSpeed += speed/accelleration;
                    }
                }
            }
            else
            {
                if (xSpeed < 0)
                {
                    xSpeed += speed/deccelleration;
                    if (xSpeed > 0)
                    {
                        xSpeed = 0;
                    }
                }
                if (xSpeed > 0)
                {
                    xSpeed -= speed/deccelleration;
                    if (xSpeed < 0)
                    {
                        xSpeed = 0;
                    }
                }
            }
            if (xVelocity < 0)
            {
                xVelocity += speed / deccelleration;
                if (xVelocity > 0)
                {
                    xVelocity = 0;
                }
            }
            if (xVelocity > 0)
            {
                xVelocity -= speed / deccelleration;
                if (xVelocity < 0)
                {
                    xVelocity = 0;
                }
            }

        }
        else
        {
            wallJumpFrame -= 1;
        }
        if (xSpeed < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (xSpeed > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        playerAnimator.SetFloat("xSpeed",Mathf.Abs(xSpeed/speed));
        
        xVelocity += xSpeed;
        
        if (TouchingGroundAtPlace(transform.position.x + xVelocity, transform.position.y, 1.2f, 0.4f)) 
        {


            TouchingGroundAtPlace(transform.position.x,transform.position.y,1.2f,0.4f);
            for (int i = 0; i < 30; i++)
            {
                if (!TouchingGroundAtPlace(transform.position.x, transform.position.y, 1.2f, 0.4f)) 
                {
                    transform.Translate(new Vector3(0.01f*Mathf.Sign(xVelocity),0,0));
                }
            }
            transform.Translate(new Vector3(-0.01f*Mathf.Sign(xVelocity),0,0));
            if (movement.y > 0)
            {
                xSpeed *= -1;
                xVelocity *= -1;
                yVelocity = jumpSpeed;
                jumpFrame = 0;
                coyoteFrame = 0;
                wallJumpFrame = wallJumpCooldown;
                playerAnimator.SetTrigger("Idle");
            }
            else
            {
                xVelocity = 0;
                xSpeed = 0;
            }
        }
        transform.Translate(new Vector3(xVelocity,0,0));
        
        
    }

    bool TouchingGroundAtPlace(float x, float y,float minHeight = 0.55f, float maxHeight = 1.0f, float width = 0.45f)
    {
        Vector2 max = new Vector2(x+width,y+maxHeight);
        Vector2 min = new Vector2(x-width,y-minHeight);
        Collider2D[] hit = Physics2D.OverlapAreaAll(max,min);
        Debug.DrawLine(new Vector3(max.x,max.y,0),new Vector3(min.x,max.y,0),Color.red);
        Debug.DrawLine(new Vector3(min.x,max.y,0),new Vector3(min.x,min.y,0),Color.red);
        Debug.DrawLine(new Vector3(min.x,min.y,0),new Vector3(max.x,min.y,0),Color.red);
        Debug.DrawLine(new Vector3(max.x,min.y,0),new Vector3(max.x,max.y,0),Color.red);
        if (hit != null)
        {
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].gameObject.tag == "Ground")
                {
                    return true;
                }
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    void SetVelocity(float newXVelocity = 0, float newYVelocity = 0, float newXSpeed = 0)
    {
        xVelocity = newXVelocity;
        yVelocity = newYVelocity;
        xSpeed = newXSpeed;
    }

}
