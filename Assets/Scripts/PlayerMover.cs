using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour {

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
	
    // move the player one space in the negative X direction
    public void MoveLeft()
    {
        Vector3 newPosition = transform.position + new Vector3(-2, 0, 0);
        Move(newPosition,0);
    }

    // move the player one space in the positive X direction
    public void MoveRight()
    {
        Vector3 newPosition = transform.position + new Vector3(2, 0, 0);
        Move(newPosition,0);
    }

    // move the player one space in the positive Z direction
    public void MoveForward()
    {
        Vector3 newPosition = transform.position + new Vector3(0, 0, 2);
        Move(newPosition,0);
    }

    // move the player one space in the negative Z direction
    public void MoveBackward()
    {
        Vector3 newPosition = transform.position + new Vector3(0, 0, -2);
        Move(newPosition,0);
    }

}
