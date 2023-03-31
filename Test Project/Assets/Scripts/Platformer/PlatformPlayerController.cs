using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public Vector2 movement;
    public Animator playerAnimator;
    private float yVelocity;
    private int coyoteFrame = 0;
    private bool grounded = false;
    private int jumpFrame = 0;
    public float jumpSpeed = 10.0f;
    public int jumpDuration = 20;
    public float gravity;
    public int coyoteTime = 0;
    public int accelleration = 3;
    public int deccelleration = 3;
    private float xVelocity = 0;
    private float xSpeed = 0;
    private Collider2D hit;
    public List<GameObject> hair;
    public GameObject hairObject;
    public int hairLength = 6;
    public Sprite hairSprite;
    public int wallJumpCooldown = 20;
    private int wallJumpFrame = 0;
    private float yMin = 0.25f;
    private float yMax = 1;

    // Start is called before the first frame update
    void Start()
    {
        //playerAnimator = GetComponent<Animator>();
        /*
        hair.Add(Instantiate(hairObject,new Vector3(transform.position.x,transform.position.y-0.1f,transform.position.z-1),transform.rotation));
        hair[0].GetComponent<HairFollow>().player = gameObject;
        hair[0].GetComponent<HairFollow>().first = true;
        */
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
        
        
        GetHitBoxAtPosition(transform.position.x,transform.position.y+yVelocity,1.3f,0.5f);

        

        grounded = false;
        if (hit != null && hit.gameObject != gameObject) 
        {
            
            
            GetHitBoxAtPosition(transform.position.x,transform.position.y,1.3f,0.5f);
            for (int i = 0; i < 30; i++)
            {
                if (hit == null) 
                {
                    transform.Translate(new Vector3(0,0.01f*Mathf.Sign(yVelocity),0),Space.World);
                    GetHitBoxAtPosition(transform.position.x,transform.position.y,1.3f,0.5f);
                }
            }
            yVelocity = 0;
            GetHitBoxAtPosition(transform.position.x,transform.position.y,1.4f,0.4f);
            if (hit != null && hit.gameObject != gameObject)
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
                if (xVelocity < 0)
                {
                    xVelocity += speed/deccelleration;
                    if (xVelocity > 0)
                    {
                        xVelocity = 0;
                    }
                }
                if (xVelocity > 0)
                {
                    xVelocity -= speed/deccelleration;
                    if (xVelocity < 0)
                    {
                        xVelocity = 0;
                    }
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
        GetHitBoxAtPosition(transform.position.x+xVelocity,transform.position.y,1.2f,0.4f);
        if (hit != null && hit.gameObject != gameObject) 
        {
            
            
            GetHitBoxAtPosition(transform.position.x,transform.position.y,1.2f,0.4f);
            for (int i = 0; i < 30; i++)
            {
                if (hit == null) 
                {
                    transform.Translate(new Vector3(0.01f*Mathf.Sign(xVelocity),0,0));
                    GetHitBoxAtPosition(transform.position.x,transform.position.y,1.2f,0.4f);
                    
                }
            }
            transform.Translate(new Vector3(-0.01f*Mathf.Sign(xVelocity),0,0));
            if (movement.y > 0)
            {
                xSpeed *= -1;
                yVelocity = jumpSpeed;
                jumpFrame = 0;
                coyoteFrame = 0;
                if (wallJumpFrame <= 0 && Mathf.Abs(xVelocity) < 0.25f)
                {
                    xVelocity *= -1.5f;
                } else {
                    xVelocity *= -1;
                }
                wallJumpFrame = wallJumpCooldown;
            }
            else
            {
                xVelocity = 0;
                xSpeed = 0;
            }
        }
        transform.Translate(new Vector3(xVelocity,0,0));
        
        
    }

    void GetHitBoxAtPosition(float x, float y,float minHeight = 0.55f, float maxHeight = 1.0f)
    {
        Vector2 max = new Vector2(x+0.45f,y+maxHeight);
        Vector2 min = new Vector2(x-0.45f,y-minHeight);
        hit = Physics2D.OverlapArea(max,min);
        Debug.DrawLine(new Vector3(max.x,max.y,0),new Vector3(min.x,max.y,0),Color.red);
        Debug.DrawLine(new Vector3(min.x,max.y,0),new Vector3(min.x,min.y,0),Color.red);
        Debug.DrawLine(new Vector3(min.x,min.y,0),new Vector3(max.x,min.y,0),Color.red);
        Debug.DrawLine(new Vector3(max.x,min.y,0),new Vector3(max.x,max.y,0),Color.red);
    }


}
