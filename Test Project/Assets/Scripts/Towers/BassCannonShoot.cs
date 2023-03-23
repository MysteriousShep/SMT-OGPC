using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BassCannonShoot : MonoBehaviour
{
    public GameObject projectile;
    public float range = 3f;
    public float fireRate = 0.5f;
    public GameObject bottom;
    private GameObject lastBass;
    private float size = 1;
    public bool playing = false;
    private GameObject cursor;
    public float fireStartDelay = 2f;

    // Loop fire method
    void Start()
    {
        cursor = GameObject.Find("Cursor");
        Instantiate(bottom,new Vector3(transform.position.x,transform.position.y,transform.position.z+0.01f),transform.rotation);
    }

    void Update()
    {
        transform.localScale = new Vector3(size,size,size);
        if (size > 1)
        {
            size = ((size-1)*0.5f)+1;
        }
        else
        {
            size = 1;
        }
        if (cursor != null)
        {
            if (!playing && !cursor.gameObject.activeSelf)
            {
                InvokeRepeating("Warn", fireStartDelay, 1f/fireRate);
                InvokeRepeating("Warn", fireStartDelay+0.3f, 1f/fireRate);
                InvokeRepeating("Warn", fireStartDelay+1f/fireRate/2f+0.15f, 1f/fireRate);
                playing = true;
            }
        }
    }
    void Warn()
    {
        Invoke("Fire",0.125f);
        size = 2;
    }
    // Shoot
    void Fire()
    {
        lastBass = Instantiate(projectile, new Vector3(transform.position.x,transform.position.y-0.2f,transform.position.z-0.1f), transform.rotation);
        Destroy(lastBass,0.25f);
    }
}
