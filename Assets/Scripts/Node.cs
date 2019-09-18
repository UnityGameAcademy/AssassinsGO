using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    // reference to mesh for display of the node
    public GameObject geometry;

    // time for scale animation to play
    public float scaleTime = 0.3f;

    // ease in-out for animation
    public iTween.EaseType easeType = iTween.EaseType.easeInExpo;

    // do we activate the animation at Start?
    public bool autoRun = false;

    // delay time before animation
    public float delay = 1f;

    void Start()
    {
        // hide the mesh by scaling to zero
        if (geometry != null)
        {
            geometry.transform.localScale = Vector3.zero;

            // play scale animation at Start
            if (autoRun)
            {
                ShowGeometry();
            }
        }
    }

    // play scale animation
    public void ShowGeometry()
    {
        if (geometry != null)
        {
            iTween.ScaleTo(geometry, iTween.Hash(
                "time", scaleTime,
                "scale", Vector3.one,
                "easetype", easeType,
                "delay", delay
            ));
        }
    }

}
