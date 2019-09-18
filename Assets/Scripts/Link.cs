using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour 
{
    // margin around each node center 
    public float borderWidth = 0.02f;

    // line thickness
    public float lineThickness = 0.5f;

    // time to scale up
    public float scaleTime = 0.25f;

    // detaly before iTween animation
    public float delay = 0.1f;

    // iTween ease type
    public iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;


    // draws the link by scaling it up from 0
    public void DrawLink(Vector3 startPos, Vector3 endPos)
    {
        // initialize the link so it has a scale of 0 in z
        transform.localScale = new Vector3(lineThickness, 1f, 0f);

        // direction from starting position to ending position
        Vector3 dirVector = endPos - startPos;

        // distance between nodes minus the borders
        float zScale = dirVector.magnitude - borderWidth * 2f;

        // the link's final scale
        Vector3 newScale = new Vector3(lineThickness, 1f, zScale);

        // rotate the link to match the direction vector orientation
        transform.rotation = Quaternion.LookRotation(dirVector);

        // move the link to start just outside the border width
        transform.position = startPos + (transform.forward * borderWidth);

        // iTween animation
        iTween.ScaleTo(gameObject, iTween.Hash(
            "time",scaleTime,
            "scale", newScale, 
            "easetype",easeType,
            "delay", delay
        ));

    }

}
