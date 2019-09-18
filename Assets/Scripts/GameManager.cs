using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	// reference to the GameBoard
	Board m_board;

	// reference to PlayerManager
	PlayerManager m_player;

	// has the user pressed start?
	bool m_hasLevelStarted = false;
	public bool HasLevelStarted { get { return m_hasLevelStarted; } set { m_hasLevelStarted = value; } }

	// have we begun gamePlay?
	bool m_isGamePlaying = false;
	public bool IsGamePlaying { get { return m_isGamePlaying; } set { m_isGamePlaying = value; } }

	// have we met the game over condition?
	bool m_isGameOver = false;
	public bool IsGameOver { get { return m_isGameOver; } set { m_isGameOver = value; } }

    // have the end level graphics finished playing?
	bool m_hasLevelFinished = false;
	public bool HasLevelFinished { get { return m_hasLevelFinished; } set { m_hasLevelFinished = value; } }

    // delay in between game stages
    public float delay = 1f;

	void Awake()
    {
        // populate Board and PlayerManager components
        m_board = Object.FindObjectOfType<Board>().GetComponent<Board>();
        m_player = Object.FindObjectOfType<PlayerManager>().GetComponent<PlayerManager>();
    }

    void Start()
    {
		// start the main game loop if the PlayerManager and Board are present
		if (m_player != null && m_board != null)
        {
            StartCoroutine("RunGameLoop");
        }
        else
        {
            Debug.LogWarning("GAMEMANAGER Error: no player or board found!");
        }
    }

	// run the main game loop, separated into different stages/coroutines
	IEnumerator RunGameLoop()
    {
        yield return StartCoroutine("StartLevelRoutine");
        yield return StartCoroutine("PlayLevelRoutine");
        yield return StartCoroutine("EndLevelRoutine");
    }

	// the initial stage after the level is loaded
	IEnumerator StartLevelRoutine()
    {
        yield return null;
    }

	// gameplay stage
	IEnumerator PlayLevelRoutine()
	{
		yield return null;
	}

	// end stage after gameplay is complete
	IEnumerator EndLevelRoutine()
	{
		yield return null;
	}
}
