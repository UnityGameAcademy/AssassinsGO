using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementType
{
    Stationary,
    Patrol,
    Spinner
}

public class EnemyMover : Mover
{
    // local direction to move (defaults to local positive z)
    public Vector3 directionToMove = new Vector3(0f, 0f, Board.spacing);

    // movement mode
    public MovementType movementType = MovementType.Stationary;

    // wait time for stationary enemies
    public float standTime = 1f;


    protected override void Awake()
    {
        base.Awake();

        // EnemyMovers always face the direction they are moving
        faceDestination = true;
    }

    protected override void Start()
    {
        base.Start();
    }

    // complete one turn of movement
    public void MoveOneTurn()
    {
        switch (movementType)
        {
            case MovementType.Patrol:
                Patrol();
                break;
            case MovementType.Stationary:
				Stand();
                break;
            case MovementType.Spinner:
                Spin();
                break;
        }
    }

    void Patrol()
    {
        StartCoroutine(PatrolRoutine());
    }

    IEnumerator PatrolRoutine()
    {
        // cache our starting position
        Vector3 startPos = new Vector3(m_currentNode.Coordinate.x, 0f, m_currentNode.Coordinate.y);

        // one space forward
        Vector3 newDest = startPos + transform.TransformVector(directionToMove);

        // two spaces forward
        Vector3 nextDest = startPos + transform.TransformVector(directionToMove * 2f);

        // move to our new destination
        Move(newDest, 0f);

        // pause until we complete the movement
        while (isMoving)
        {
			yield return null; 
        }

        // check if we have reached a deadend
        if (m_board != null)
        {
            // our destination Node
            Node newDestNode = m_board.FindNodeAt(newDest);

            // the Node two spaces away
            Node nextDestNode = m_board.FindNodeAt(nextDest);

            // if the Node two spaces away does not exist OR is not connected to our destination Node...
            if (nextDestNode == null || !newDestNode.LinkedNodes.Contains(nextDestNode))
            {
                // turn to face our original Node and set that as our new destination
                destination = startPos;
                FaceDestination();

                // wait until we are done rotating
                yield return new WaitForSeconds(rotateTime);
            }
        }

		// broadcast message at end of movement
		base.finishMovementEvent.Invoke();
    }

    // movement turn for stationary enemies
    void Stand()
    {
        StartCoroutine(StandRoutine());
    }

    // routine for stationary movement
    IEnumerator StandRoutine()
    {
        // time to wait
        yield return new WaitForSeconds(standTime);

        // broadcast message at end of movement
        base.finishMovementEvent.Invoke();
    }

    void Spin()
    {
        StartCoroutine(SpinRoutine());
    }

    IEnumerator SpinRoutine()
    {
        // local z forward
        Vector3 localForward = new Vector3(0f, 0f, Board.spacing);

        // destination is always one space directly behind us
        destination = transform.TransformVector(localForward * -1f) + transform.position;

        // rotate 180 degrees
        FaceDestination();

        // wait for rotation to finish
        yield return new WaitForSeconds(rotateTime);

		// broadcast message at end of movement
		base.finishMovementEvent.Invoke();
    }
}
