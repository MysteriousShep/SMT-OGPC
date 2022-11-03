using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
    
    private Animator anim;
    private string currentState;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // method that changes animation state
    public void changeAnimationState(string newState)
    {
        if (newState == currentState)
        {
            return;
        }
        anim.Play(newState);
        currentState = newState;
    }
}
