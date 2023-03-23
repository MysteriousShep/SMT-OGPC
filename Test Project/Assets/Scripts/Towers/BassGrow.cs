using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BassGrow : MonoBehaviour
{
    public float growDelay = 0.0f;
    public float growAmount = 0.2f;
    private float alpha = 1.0f;
    void Start()
    {
        InvokeRepeating("Grow",growDelay,0.05f);
    }
    void Grow()
    {
        transform.localScale = new Vector3((transform.localScale.x*growAmount),(transform.localScale.y*growAmount),1);
        alpha = Mathf.Max(0,alpha-0.25f);
        GetComponent<SpriteRenderer>().color = new Color(1,1,1,alpha);
    }
}
