using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    // offset to put the enemy offscreen (defaults to 10 units directly above)
    public Vector3 offscreenOffset = new Vector3(0f, 10f, 0f);

    // reference to Board component
    Board m_board;

    // delay before enemy death
    public float deathDelay = 0f;

    // how long to hold the gamepiece offscreen before we drop it onto the capture position
    public float offscreenDelay = 1f;

    // iTween animation delay
    public float iTweenDelay = 0f;

    // iTween ease in-out
    public iTween.EaseType easeType = iTween.EaseType.easeInOutQuint;

    // time to move the enemy
    public float moveTime = 0.5f;

    void Awake()
    {
        m_board = Object.FindObjectOfType<Board>().GetComponent<Board>();
    }

    // move the enemy to a target position
    public void MoveOffBoard(Vector3 target)
    {
        iTween.MoveTo(gameObject, iTween.Hash(
            "x", target.x,
            "y", target.y,
            "z", target.z,
            "delay", iTweenDelay,
            "easetype", easeType,
            "time", moveTime
        ));
    }

	// start the enemy death routine
	public void Die()
    {
        StartCoroutine(DieRoutine());
    }

    IEnumerator DieRoutine()
    {
        // wait for a short delay before the enemy death
        yield return new WaitForSeconds(deathDelay);

        // get an offscreen position directly above us off camera
        Vector3 offscreenPos = transform.position + offscreenOffset;

        // move the enemy off camera 
        MoveOffBoard(offscreenPos);

        // wait for the animation to finish and add an extra delay
        yield return new WaitForSeconds(moveTime + offscreenDelay);

        // if the capturePosition index is valid...
        if (m_board.capturePositions.Count != 0 
            && m_board.CurrentCapturePosition < m_board.capturePositions.Count)
        {
            // select the corresponding capture position
            Vector3 capturePos = m_board.capturePositions[m_board.CurrentCapturePosition].position;

            // move the enemy directly over the capture position
            transform.position = capturePos + offscreenOffset;

            // drop the enemy down onto the capture position
            MoveOffBoard(capturePos);

            // wait for the animation to finish
            yield return new WaitForSeconds(moveTime);

            // increment the current index and verify the index is valid
            m_board.CurrentCapturePosition++;
            m_board.CurrentCapturePosition = 
                Mathf.Clamp(m_board.CurrentCapturePosition, 0, m_board.capturePositions.Count - 1);
        }
    }
}
