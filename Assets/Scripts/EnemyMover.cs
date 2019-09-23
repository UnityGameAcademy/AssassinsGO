using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : Mover
{
    protected override void Awake()
    {
        base.Awake();

        // EnemyMovers always face the direction they are moving
        faceDestination = true;
    }

    protected override void Start()
    {
        base.Start();
        //StartCoroutine(TestMovementRoutine());
    }

	//IEnumerator TestMovementRoutine()
	//{
	//	yield return new WaitForSeconds(5f);
	//	MoveForward();

	//	yield return new WaitForSeconds(2f);
	//	MoveRight();

	//	yield return new WaitForSeconds(2f);
 //       MoveForward();

	//	yield return new WaitForSeconds(2f);
	//	MoveForward();

	//	yield return new WaitForSeconds(2f);
 //       MoveBackward();

	//	yield return new WaitForSeconds(2f);
	//	MoveBackward();
	//}


}
