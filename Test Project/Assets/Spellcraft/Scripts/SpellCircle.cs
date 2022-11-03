using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCircle : MonoBehaviour
{
    public GameObject[] anchors;
    public LineRenderer line;
    public float outerTurnDegrees = 90;
    public float outerStartOffset = -45;
    public float innerTurnDegrees = 90;
    public float innerStartOffset = -90;
    public bool GreenPositive = false;
    public bool BluePositive = false;

    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        float color = 0.0f;
        GameObject anchor = anchors[0];
        // Get circle size
        float outerSize = Vector3.Distance(new Vector3(0,0,0),anchor.transform.position);
        float innerSize = Vector3.Distance(new Vector3(0,0,0),anchors[1].transform.position);
        // loop through the first half of the anchors, setting their positions to [outerSize] away from 0 0 in a circle
        transform.Rotate(new Vector3(0,0,outerStartOffset));
        for (int anchorIndex = 1; anchorIndex < anchors.Length; anchorIndex += 2)
        {
            anchor = anchors[anchorIndex];
            // Set position to 0 0, turn 90 degrees, move forwards [outerSize] units
            transform.position = new Vector3(0,0,0);
            transform.Rotate(new Vector3(0,0,outerTurnDegrees));
            transform.Translate(Vector3.right*outerSize);
            // Set line position
            line.SetPosition(anchorIndex,transform.position);
            color += Vector3.Distance(new Vector3(0,0,0),transform.position);
        }
        
        // Reset position
        transform.eulerAngles = new Vector3(0,0,0);
        transform.position = new Vector3(0,0,0);

        // loop through the second half of the anchors, setting their positions to [outerSize] away from 0 0 in a circle
        transform.Rotate(new Vector3(0,0,innerStartOffset));
        for (int anchorIndex = 0; anchorIndex < anchors.Length; anchorIndex += 2)
        {
            anchor = anchors[anchorIndex];
            // Set position to 0 0, turn 90 degrees, move forwards [outerSize] units
            transform.position = new Vector3(0,0,0);
            transform.Rotate(new Vector3(0,0,innerTurnDegrees));
            transform.Translate(Vector3.right*innerSize);
            // Set line position
            line.SetPosition(anchorIndex,transform.position);
            color -= Vector3.Distance(new Vector3(0,0,0),transform.position);
        }

        // Reset position
        transform.eulerAngles = new Vector3(0,0,0);
        transform.position = new Vector3(0,0,0);
        
        // Set line color
        Color newColor = new Color(Mathf.Clamp((color)/30,0,1),1-Mathf.Clamp((color+10)/30,0,1),1-Mathf.Clamp((color+10)/30,0,1),1);
        if (GreenPositive) 
        {
            newColor.g = Mathf.Clamp((color-10)/30,0,1);
        }
        if (BluePositive)
        {
            newColor.b = Mathf.Clamp((color-10)/30,0,1);
        }
        line.endColor = newColor;
        line.startColor = line.endColor;
        
    }
}
