using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDecay : MonoBehaviour
{
    public float shrinkDelay = 0.2f;
    public float shrinkMultiplier = 0.6f;
    void Start()
    {
        InvokeRepeating("Shrink",shrinkDelay,0.05f);
    }
    void Shrink()
    {
        transform.localScale = new Vector3(40,(transform.localScale.y*shrinkMultiplier),1);
    }
}
