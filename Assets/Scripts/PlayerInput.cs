using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // store horizontal input
    public float h;

    // store the vertical input 
    public float v;

    // global flag for enabling and disabling user input
    public bool inputEnabled = false;

    // get keyboard input
    public void GetKeyInput()
    {
        // if input is enabled, just get the raw axis data from the Horizontal and Vertical virtual axes (defined in InputManager)
        if (inputEnabled)
        {
			h = Input.GetAxisRaw("Horizontal");
			v = Input.GetAxisRaw("Vertical");  
        }
    }
}
