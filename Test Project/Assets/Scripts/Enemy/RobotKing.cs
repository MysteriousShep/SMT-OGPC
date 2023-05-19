using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotKing : MonoBehaviour
{
    
    public int hp = 5;
    public GameObject player;
    public bool dead = false;
    private int iFrame = 0;
    public GameObject end;
    
    void FixedUpdate()
    {
        iFrame -= 1;
        if (TouchingTagAtPlace(transform.position.x,transform.position.y,50f,30f,8f,"PlayerAttack") && !dead && iFrame < 0)
        {
            hp -= 1;
            iFrame = 20;
                player.GetComponent<PlatformPlayerController>().SetVelocity(player.GetComponent<PlatformPlayerController>().xVelocity,player.GetComponent<PlayerAttack>().knockbackAmount,player.GetComponent<PlatformPlayerController>().xSpeed);
                player.GetComponent<PlatformPlayerController>().jumpFrame = 0;
                player.GetComponent<PlatformPlayerController>().coyoteFrame = 0;

            
            if (player.GetComponent<PlayerAttack>().positionOffset.x < 0)
            {
                player.GetComponent<PlatformPlayerController>().SetVelocity(player.GetComponent<PlatformPlayerController>().xVelocity,player.GetComponent<PlatformPlayerController>().yVelocity,player.GetComponent<PlatformPlayerController>().xSpeed+player.GetComponent<PlayerAttack>().knockbackAmount);
            }
            if (player.GetComponent<PlayerAttack>().positionOffset.x > 0)
            {
                player.GetComponent<PlatformPlayerController>().SetVelocity(player.GetComponent<PlatformPlayerController>().xVelocity,player.GetComponent<PlatformPlayerController>().yVelocity,player.GetComponent<PlatformPlayerController>().xSpeed-player.GetComponent<PlayerAttack>().knockbackAmount);
            }
            if (hp <= 0) {
                dead = true;
                GetComponent<Animator>().SetTrigger("Defeat");
                Instantiate(end,transform.position,transform.rotation);
            }
        }
        if (!dead) {
            if (TouchingTagAtPlace(transform.position.x,transform.position.y,50f,30f,8f,"Friendly")) {
                player.GetComponent<GetHit>().Hit(10);
                if (player.transform.position.x < transform.position.x)
                {
                    player.GetComponent<PlatformPlayerController>().SetVelocity(1f,player.GetComponent<PlatformPlayerController>().yVelocity,1f);
                }
                if (player.transform.position.x > transform.position.x)
                {
                    player.GetComponent<PlatformPlayerController>().SetVelocity(-1f,player.GetComponent<PlatformPlayerController>().yVelocity,-1f);
                }
                player.GetComponent<PlatformPlayerController>().SetVelocity(player.GetComponent<PlatformPlayerController>().xVelocity,0.5f,player.GetComponent<PlatformPlayerController>().xSpeed);
            }
        }
    }


    bool TouchingTagAtPlace(float x, float y, float minHeight = 0.55f, float maxHeight = 1.0f, float width = 0.45f, string target = "Ground")
    {
        Vector2 max = new Vector2(x + width, y + maxHeight);
        Vector2 min = new Vector2(x - width, y - minHeight);
        Collider2D[] hit = Physics2D.OverlapAreaAll(max, min);
        Debug.DrawLine(new Vector3(max.x, max.y, 0), new Vector3(min.x, max.y, 0), Color.red);
        Debug.DrawLine(new Vector3(min.x, max.y, 0), new Vector3(min.x, min.y, 0), Color.red);
        Debug.DrawLine(new Vector3(min.x, min.y, 0), new Vector3(max.x, min.y, 0), Color.red);
        Debug.DrawLine(new Vector3(max.x, min.y, 0), new Vector3(max.x, max.y, 0), Color.red);
        if (hit != null)
        {
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].gameObject.tag == target)
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
}
