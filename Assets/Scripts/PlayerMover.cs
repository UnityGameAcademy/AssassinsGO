﻿using System.Collections;
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

    Board m_board;

    // Use this for initialization
    void Awake()
    {
        m_board = Object.FindObjectOfType<Board>().GetComponent<Board>();
    }

    void Start()
    {
        UpdateBoard();
    }

    // public method to invole the MoveRoutine
    public void Move(Vector3 destinationPos, float delayTime = 0.25f)
    {
        // only move if the destination is at a valid Node
        if (m_board != null)
        {
            Node targetNode = m_board.FindNodeAt(destinationPos);

            if (targetNode != null && m_board.PlayerNode.LinkedNodes.Contains(targetNode))
            {
                // start the coroutine MoveRoutine
                StartCoroutine(MoveRoutine(destinationPos, delayTime));
            }
        }
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
            "easetype", easeType,
            "speed", moveSpeed
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

        UpdateBoard();
    }

    // move the player one space in the negative X direction
    public void MoveLeft()
    {
        Vector3 newPosition = transform.position + new Vector3(-Board.spacing, 0f, 0f);
        Move(newPosition, 0);
    }

    // move the player one space in the positive X direction
    public void MoveRight()
    {
        Vector3 newPosition = transform.position + new Vector3(Board.spacing, 0f, 0f);
        Move(newPosition, 0);
    }

    // move the player one space in the positive Z direction
    public void MoveForward()
    {
        Vector3 newPosition = transform.position + new Vector3(0f, 0f, Board.spacing);
        Move(newPosition, 0);
    }

    // move the player one space in the negative Z direction
    public void MoveBackward()
    {
        Vector3 newPosition = transform.position + new Vector3(0f, 0f, -Board.spacing);
        Move(newPosition, 0);
    }

    void UpdateBoard()
    {
        if (m_board != null)
        {
            m_board.UpdatePlayerNode();
        }
    }
}
