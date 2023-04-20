using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour
{
    public float speed = 0.1f;
    public float yVelocity = 0;
    public float gravity = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (TouchingGroundAtPlace(transform.position.x, transform.position.y + yVelocity, 1f, 0.5f))
        {


            TouchingGroundAtPlace(transform.position.x, transform.position.y, 1f, 0.5f);
            for (int i = 0; i < 30; i++)
            {
                if (!TouchingGroundAtPlace(transform.position.x, transform.position.y, 1f, 0.5f))
                {
                    transform.Translate(new Vector3(0, 0.01f * Mathf.Sign(yVelocity), 0), Space.World);

                }
            }
            yVelocity = 0;

        }

        yVelocity -= gravity * Time.fixedDeltaTime;
        transform.position = new Vector3(transform.position.x+speed,transform.position.y,0);
        if (!TouchingGroundAtPlace(transform.position.x + speed, transform.position.y, 1.25f, 0, 0.5f))
        {
            speed *= -1;
        }

    }


    bool TouchingGroundAtPlace(float x, float y, float minHeight = 0.55f, float maxHeight = 1.0f, float width = 0.45f)
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
}
