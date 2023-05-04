using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerLevelEnd : MonoBehaviour
{
    void FixedUpdate()
    {
        Vector2 max = new Vector2(transform.position.x+0.45f,transform.position.y+0.5f);
        Vector2 min = new Vector2(transform.position.x-0.45f,transform.position.y-1.3f);
        Collider2D[] hit = Physics2D.OverlapAreaAll(max,min);
        Debug.DrawLine(new Vector3(max.x,max.y,0),new Vector3(min.x,max.y,0),Color.green);
        Debug.DrawLine(new Vector3(min.x,max.y,0),new Vector3(min.x,min.y,0),Color.green);
        Debug.DrawLine(new Vector3(min.x,min.y,0),new Vector3(max.x,min.y,0),Color.green);
        Debug.DrawLine(new Vector3(max.x,min.y,0),new Vector3(max.x,max.y,0),Color.green);
        if (hit != null)
        {
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].gameObject.tag == "LevelEnd")
                {
                    Debug.Log("AAA");
                    
                    Vector2 newVelocity = new Vector2(transform.position.x-GetComponent<PlatformPlayerController>().lastGroundedPosition.x,transform.position.y-GetComponent<PlatformPlayerController>().lastGroundedPosition.y);
                    float newXVelocity = newVelocity.x;
                    float newYVelocity = newVelocity.y;
                    //GetComponent<PlatformPlayerController>().SetVelocity(0.5f*Mathf.Sign(newXVelocity/-20),0.5f,0);
                    transform.position = GetComponent<PlatformPlayerController>().lastGroundedPosition;
                }
            }
            
        }
    }
    
}
