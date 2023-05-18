using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackHitBox;
    public int attackCooldown = 30;
    public int attackLength = 30;
    private int attackFrame = 0;
    public Vector3 positionOffset = new Vector3(0, 0, 0);
    public float verticalAttackDistance = 2;
    public float horizontalAttackDistance = 4;
    public GameObject camera;
    public PlatformPlayerController velocity;
    public float knockbackAmount = 0.5f;
    public List<string> paradoxes;
    public TextMeshProUGUI attackText;
    private int paradoxIndex = 0;

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
            paradoxIndex = Random.Range(0,paradoxes.Count);
            attackHitBox.SetActive(true);
            attackFrame = attackCooldown;
            attackHitBox.transform.position = transform.position;
            positionOffset = new Vector3(0, 0, 0);
            camera.GetComponent<Screenshake>().screenshake = Mathf.Max(camera.GetComponent<Screenshake>().screenshake, 4);
            attackText.text = paradoxes[paradoxIndex];
        }
        if (attackFrame < attackCooldown - attackLength)
        {
            attackHitBox.SetActive(false);
        }
        else
        {
            if (attackFrame > attackLength - 2)
            {
                if (Input.GetAxis("Horizontal") > 0)
                {
                    positionOffset = new Vector3(horizontalAttackDistance, 0, 0);
                }
                if (Input.GetAxis("Horizontal") < 0)
                {
                    positionOffset = new Vector3(-horizontalAttackDistance, 0, 0);
                }
                if (Input.GetAxis("Vertical") > 0)
                {
                    positionOffset = new Vector3(0, verticalAttackDistance, 0);
                }
                if (Input.GetAxis("Vertical") < 0)
                {
                    positionOffset = new Vector3(0, -verticalAttackDistance, 0);
                }
            }
            attackHitBox.transform.position = transform.position + positionOffset;

        }
    }
}
