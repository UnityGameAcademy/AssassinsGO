using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
     // where the player is currently headed 
    public Vector3 destination;

    // is the player currently moving?
    public bool isMoving = false;

    // what easetype to use for iTweening
    public iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;

    // how fast we move
    public float moveSpeed = 1.5f;

    // delay to use before any call to iTween
    public float iTweenDelay = 0f;

	// Use this for initialization
	void Start () 
    {
        // temporarily invoke Move 
        Move(new Vector3(2f, 0f, 0f), 1f);
        Move(new Vector3(4f, 0f, 0f), 3f);
        Move(new Vector3(4f, 0f, 2f), 5f);
        Move(new Vector3(4f, 0f, 4f), 7f);
	}

    // public method to invole the MoveRoutine
    public void Move(Vector3 destinationPos, float delayTime = 0.25f)
    {
        // start the coroutine MoveRoutine
        StartCoroutine(MoveRoutine(destinationPos, delayTime));

    }

    // coroutine used to move the player
    IEnumerator MoveRoutine(Vector3 destinationPos, float delayTime)
    {
        // we are moving
        isMoving = true;

        // set the destination to the destinationPos being passed into the coroutine
        destination = destinationPos;

        // pause the coroutine for a brief periof
        yield return new WaitForSeconds(delayTime);

        // move the player toward the destinationPos using the easeType and moveSpeed variables
        iTween.MoveTo(gameObject, iTween.Hash(
            "x", destinationPos.x,
            "y", destinationPos.y,
            "z", destinationPos.z,
            "delay", iTweenDelay,
            "easetype",easeType,
            "speed",moveSpeed
        ));

        while (Vector3.Distance(destinationPos, transform.position) > 0.01f)
        {
            yield return null;
        }

        // stop the iTween immediately
        iTween.Stop(gameObject);

        // set the player position to the destination explicitly
        transform.position = destinationPos;

        // we are not moving
        isMoving = false;
    
    }

}
