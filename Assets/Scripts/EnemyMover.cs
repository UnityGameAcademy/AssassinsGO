using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : Mover
{
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
        Stand();
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

}
