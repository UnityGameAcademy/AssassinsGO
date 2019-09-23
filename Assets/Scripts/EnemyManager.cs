using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemySensor))]
public class EnemyManager : TurnManager
{
	// reference to EnemyMover component
    EnemyMover m_enemyMover;

    // reference to EnemySensor component
    EnemySensor m_enemySensor;

    // reference to Board component
    Board m_board;

    // setup member variables
    protected override void Awake()
    {
        base.Awake();

        m_board = Object.FindObjectOfType<Board>().GetComponent<Board>();
        m_enemyMover = GetComponent<EnemyMover>();
        m_enemySensor = GetComponent<EnemySensor>();

    }
}
