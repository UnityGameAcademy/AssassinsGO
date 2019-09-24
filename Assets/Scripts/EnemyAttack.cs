using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

   // reference to PlayerManager component
    PlayerManager m_player;

    void Awake()
    {
        m_player = Object.FindObjectOfType<PlayerManager>().GetComponent<PlayerManager>();
    }

    // kill the player
    public void Attack()
    {
        if (m_player != null)
        {
            m_player.Die();
        }
    }
}
