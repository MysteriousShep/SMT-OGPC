using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 movement = new Vector2(0,0);
    public float speed = 5.0f;
    public float turnSpeed = 20.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
    }
    void FixedUpdate()
    {
        
        float angle = Vector2.SignedAngle(Vector2.right,movement);
        
        Vector3 targetRotation = new Vector3(0, 0, angle);
        if (GetComponent<Rigidbody2D>().velocity == new Vector2(0,0)) {
            angle = Vector2.SignedAngle(Vector2.right,movement);
        
            targetRotation = new Vector3(0, 0, angle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation),360);
        }
        
        GetComponent<Rigidbody2D>().velocity = movement*speed;
        
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), turnSpeed);
        if (movement == new Vector2(0,0)) {
            transform.localRotation = new Quaternion(0,0,0,0);
        }
    }
}
