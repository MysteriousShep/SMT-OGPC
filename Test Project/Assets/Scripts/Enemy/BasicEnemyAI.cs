using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour
{
    public float speed = 0.1f;
    public float yVelocity = 0;
    public float gravity = 2;
    public int hp = 1;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (TouchingTagAtPlace(transform.position.x, transform.position.y + yVelocity, 1f, 0.5f))
        {


            TouchingTagAtPlace(transform.position.x, transform.position.y, 1f, 0.5f);
            for (int i = 0; i < 30; i++)
            {
                if (!TouchingTagAtPlace(transform.position.x, transform.position.y, 1f, 0.5f))
                {
                    transform.Translate(new Vector3(0, 0.01f * Mathf.Sign(yVelocity), 0), Space.World);

                }
            }
            yVelocity = 0;

        }

        yVelocity -= gravity * Time.fixedDeltaTime;
        transform.position = new Vector3(transform.position.x+speed,transform.position.y,0);
        if (!TouchingTagAtPlace(transform.position.x + speed, transform.position.y, 1.25f, 1f, 0.5f) || (TouchingTagAtPlace(transform.position.x + speed, transform.position.y, 0.9f, 0, 0.5f)))
        {
            speed *= -1;
        }
        if (TouchingTagAtPlace(transform.position.x,transform.position.y,1.25f,1.5f,0.6f,"PlayerAttack"))
        {
            hp -= 1;
            if (player.GetComponent<PlayerAttack>().positionOffset.y < 0)
            {
                player.GetComponent<PlatformPlayerController>().SetVelocity(player.GetComponent<PlatformPlayerController>().xVelocity,player.GetComponent<PlayerAttack>().knockbackAmount,player.GetComponent<PlatformPlayerController>().xSpeed);
            }
            if (player.GetComponent<PlayerAttack>().positionOffset.x < 0)
            {
                player.GetComponent<PlatformPlayerController>().SetVelocity(player.GetComponent<PlatformPlayerController>().xVelocity+player.GetComponent<PlayerAttack>().knockbackAmount,player.GetComponent<PlatformPlayerController>().yVelocity,player.GetComponent<PlatformPlayerController>().xSpeed);
            }
            if (player.GetComponent<PlayerAttack>().positionOffset.x > 0)
            {
                player.GetComponent<PlatformPlayerController>().SetVelocity(player.GetComponent<PlatformPlayerController>().xVelocity-player.GetComponent<PlayerAttack>().knockbackAmount,player.GetComponent<PlatformPlayerController>().yVelocity,player.GetComponent<PlatformPlayerController>().xSpeed);
            }
            if (hp <= 0) {
                Destroy(gameObject);
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
