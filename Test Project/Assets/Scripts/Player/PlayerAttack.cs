using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackHitBox;
    public int attackLength = 30;
    private int attackFrame = 0;
    private Vector3 positionOffset = new Vector3(0, 0, 0);
    public float attackDistance = 2;
    public GameObject camera;
    public PlatformPlayerController velocity;

    void Start()
    {
        attackHitBox.SetActive(false);
        velocity = GetComponent<PlatformPlayerController>();
    }

    void FixedUpdate()
    {
        attackFrame -= 1;
        
    }
    void LateUpdate()
    {
        if (Input.GetButton("Fire1") && attackFrame < 0)
        {
            attackHitBox.SetActive(true);
            attackFrame = attackLength;
            attackHitBox.transform.position = transform.position;
            positionOffset = new Vector3(0, 0, 0);
            camera.GetComponent<Screenshake>().screenshake = Mathf.Max(camera.GetComponent<Screenshake>().screenshake, 4);
        }
        if (attackFrame < 0)
        {
            attackHitBox.SetActive(false);
        }
        else
        {
            if (attackFrame > attackLength - 2)
            {
                if (Input.GetAxis("Horizontal") > 0)
                {
                    positionOffset = new Vector3(attackDistance, 0, 0);
                }
                if (Input.GetAxis("Horizontal") < 0)
                {
                    positionOffset = new Vector3(-attackDistance, 0, 0);
                }
                if (Input.GetAxis("Vertical") > 0)
                {
                    positionOffset = new Vector3(0, attackDistance, 0);
                }
                if (Input.GetAxis("Vertical") < 0)
                {
                    positionOffset = new Vector3(0, -attackDistance, 0);
                }
            }
            attackHitBox.transform.position = transform.position + positionOffset;
        }
    }
}
