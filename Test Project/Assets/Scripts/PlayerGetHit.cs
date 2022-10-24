using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetHit : MonoBehaviour
{
    public Rigidbody2D rb;
    public float bounceHeight = 10.0f;
    public GameObject[] enemyTypes;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if (rb.velocity.y < 0)
            {
                GameObject defeatedEnemy = Instantiate(enemyTypes[0],new Vector3(other.transform.position.x,other.transform.position.y-0.25f,0),other.transform.rotation);
                Destroy(other.gameObject);
                rb.velocity = new Vector2(rb.velocity.x,bounceHeight);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
