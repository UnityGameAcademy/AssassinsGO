using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerManager : TurnManager
{
	// reference to PlayerMover and PlayerInput components
	public PlayerMover playerMover;
    public PlayerInput playerInput;

    protected override void Awake()
    {
        base.Awake();

		// cache references to PlayerMover and PlayerInput
		playerMover = GetComponent<PlayerMover>();
        playerInput = GetComponent<PlayerInput>();

		// make sure that input is enabled when we begin
		playerInput.InputEnabled = true;
    }

    void Update()
    {
		// if the player is currently moving or if it's not the Player's turn, ignore user input
        if (playerMover.isMoving || m_gameManager.CurrentTurn != Turn.Player)
        {
            return;
        }

		// get keyboard input
		playerInput.GetKeyInput();

		// connect user input with PlayerMover's Move methods
		if (playerInput.V == 0)
        {
            if (playerInput.H < 0)
            {
                playerMover.MoveLeft();
            }
            else if (playerInput.H > 0)
            {
                playerMover.MoveRight();
            }
        }
        else if (playerInput.H == 0)
        {
            if (playerInput.V < 0)
            {
                playerMover.MoveBackward();
            }
            else if (playerInput.V > 0)
            {
                playerMover.MoveForward();
            }
        }
    }
}
