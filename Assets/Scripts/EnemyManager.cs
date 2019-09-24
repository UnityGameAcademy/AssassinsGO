using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemySensor))]
[RequireComponent(typeof(EnemyAttack))]
public class EnemyManager : TurnManager
{
    // reference to EnemyMover component
    EnemyMover m_enemyMover;

    // reference to EnemySensor component
    EnemySensor m_enemySensor;

    EnemyAttack m_enemyAttack;

    // reference to Board component
    Board m_board;

    // are we dead yet?
    bool m_isDead = false;
    public bool IsDead { get { return m_isDead; }}

    // actions to invoke upon enemy death
    public UnityEvent deathEvent;

    // setup member variables
    protected override void Awake()
    {
        base.Awake();

        m_board = Object.FindObjectOfType<Board>().GetComponent<Board>();
        m_enemyMover = GetComponent<EnemyMover>();
        m_enemySensor = GetComponent<EnemySensor>();
        m_enemyAttack = GetComponent<EnemyAttack>();

    }

    // play the Enemy's turn routine
    public void PlayTurn()
    {
        // if dead, finish the turn manually and disable enemy behavior
        if (m_isDead)
        {
            FinishTurn();
            return;
        }

        StartCoroutine(PlayTurnRoutine());
    }

    // main enemy routine: detect/attack Player if possible...then move/wait
    IEnumerator PlayTurnRoutine()
    {
        if (m_gameManager != null && !m_gameManager.IsGameOver)
        {
            // detect player
            m_enemySensor.UpdateSensor(m_enemyMover.CurrentNode);

            // wait
            yield return new WaitForSeconds(0f);

            if (m_enemySensor.FoundPlayer)
            {
				// notify the GameManager to lose the level
				m_gameManager.LoseLevel();

                // the player's position
                Vector3 playerPosition = new Vector3(m_board.PlayerNode.Coordinate.x, 0f,
                                                     m_board.PlayerNode.Coordinate.y);
                // move to the Player's position
                m_enemyMover.Move(playerPosition, 0f);

                // wait for the enemy iTween animation to finish
                while (m_enemyMover.isMoving)
                {
                    yield return null;
                }

                // attack/kill player   
                m_enemyAttack.Attack();


            }
            else
            {
                // movement
                m_enemyMover.MoveOneTurn();
            }
        }
    }

    // invoke the death event if we are not already dead
    public void Die()
    {
        if (m_isDead)
        {
            return;
        }

        m_isDead = true;

        if (deathEvent != null)
        {
            deathEvent.Invoke();
        }
    }
}
