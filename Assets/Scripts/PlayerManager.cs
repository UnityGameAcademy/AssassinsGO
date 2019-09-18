using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerManager : MonoBehaviour
{
	// reference to PlayerMover and PlayerInput components
	public PlayerMover playerMover;
    public PlayerInput playerInput;

    void Awake()
    {
		// cache references to PlayerMover and PlayerInput
		playerMover = GetComponent<PlayerMover>();
        playerInput = GetComponent<PlayerInput>();

		// make sure that input is enabled when we begin
		playerInput.InputEnabled = true;
    }

    void Update()
    {
		// if the player is currently moving, ignore user input
		if (playerMover.isMoving)
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
