using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCircle : MonoBehaviour
{
    public GameObject[] anchors;
    public LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        float color = 0.0f;
        GameObject anchor = anchors[0];
        for (int anchorIndex = 0; anchorIndex < anchors.Length; anchorIndex += 1)
        {
            anchor = anchors[anchorIndex];
            line.SetPosition(anchorIndex,anchor.transform.position);
            color += Mathf.Sqrt(Mathf.Pow(anchor.transform.position.x,2)+Mathf.Pow(anchor.transform.position.y,2));
        }
        Debug.Log(color);
    }
}
