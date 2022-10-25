using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public bool movingLeft = true;
    public Rigidbody2D rb;
    public float speed = 4;
    public BoxCollider2D hitbox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 max = hitbox.bounds.max;
        Vector3 min = hitbox.bounds.min;
        Vector2 corner1 = new Vector2(min.x-0.05f,max.y-0.1f);
        Vector2 corner2 = new Vector2(min.x-0.1f,min.y+0.1f);
        Collider2D hit = Physics2D.OverlapArea(corner1,corner2);
        if (hit != null)
        {
            if (hit.tag == "Ground")
            {
                movingLeft = !movingLeft;
            }
        }
        else
        {
            corner1 = new Vector2(max.x+0.05f,max.y-0.1f);
            corner2 = new Vector2(max.x+0.1f,min.y+0.1f);
            hit = Physics2D.OverlapArea(corner1,corner2);
            if (hit != null)
            {
                if (hit.tag == "Ground")
                {
                    movingLeft = !movingLeft;
                }
            }
        }
        if (movingLeft) {
            rb.velocity = new Vector2(-speed,rb.velocity.y);
        } else {
            rb.velocity = new Vector2(speed,rb.velocity.y);
        }
        
    }
}
