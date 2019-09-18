using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // store horizontal input
    float m_h;
    public float H { get { return m_h; } }

    // store the vertical input 
    float m_v;
    public float V { get { return m_v; } }

    // global flag for enabling and disabling user input
    bool m_inputEnabled = false;
    public bool InputEnabled { get { return m_inputEnabled; } set { m_inputEnabled = value; } }

    // get keyboard input
    public void GetKeyInput()
    {
        // if input is enabled, just get the raw axis data from the Horizontal and Vertical virtual axes (defined in InputManager)
        if (m_inputEnabled)
        {
            m_h = Input.GetAxisRaw("Horizontal");
            m_v = Input.GetAxisRaw("Vertical");
        }
        // if input is disabled, ensure that extra key input does not cause unintended movement
        else
        {
            m_h = 0f;
            m_v = 0f;
        }
    }

}
