using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    // speed in degrees per second
    public float rotateSpeed = 20f;

    // iTween animation to spin the object around at constant rate in a loop
    void Start()
    {
        iTween.RotateBy(gameObject, iTween.Hash(
            "y", 360f,
            "looptype", iTween.LoopType.loop,
            "speed", rotateSpeed,
            "easetype", iTween.EaseType.linear
        
        ));
    }

}
