using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
	// reference to AnimatorController
    public Animator playerAnimController;

    // string id for PlayerDeath trigger parameter
    public string playerDeathTrigger = "IsDead";

    // play the death animation
    public void Die()
    {
        if (playerAnimController != null)
        {
            playerAnimController.SetTrigger(playerDeathTrigger);
        }
    }

}
