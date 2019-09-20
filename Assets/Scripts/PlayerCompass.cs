using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCompass : MonoBehaviour
{

	// reference to the Board (to use the PlayerNode)
	Board m_board;

	// list of the arrows that make up the compass
	List<GameObject> m_arrows = new List<GameObject>();

	// prefab used to instantiate the arrows
	public GameObject arrowPrefab;

	// scale adjustment
	public float scale = 1f;

	// starting distance from center in local z
	public float startDistance = 0.25f;

	// ending distance from center in local z
	public float endDistance = 0.5f;

	// iTween animation time
	public float moveTime = 1f;

	// iTween ease in-out
	public iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;

	// delay for iTween animation
	public float delay = 0f;

    void Awake()
    {
		// cache a reference to the Board class
		m_board = GameObject.FindObjectOfType<Board>().GetComponent<Board>();

		// create the arrow heads
		SetupArrows();
    }

    void SetupArrows()
    {
		// warning if the prefab is not defined in the Inspector
		if (arrowPrefab == null)
        {
            Debug.LogWarning("PLAYERCOMPASS SetupArrows ERROR: Missing arrow prefab!");
            return;
        }

		// create one arrow for each direction vector in the Board.directions
		foreach (Vector2 dir in Board.directions)
		{
			// create a normalized 3d Vector from the Board's 2d Vector
			Vector3 dirVector = new Vector3(dir.normalized.x, 0f, dir.normalized.y);

			// create a rotation based on this 3d Vector
			Quaternion rotation = Quaternion.LookRotation(dirVector);

			// create an arrow head with the startDistance offset from center using our calculated rotation
			GameObject arrowInstance = Instantiate(arrowPrefab, transform.position + dirVector * startDistance, rotation);

			// scale the arrow head if it needs a scale adjustment
			arrowInstance.transform.localScale = new Vector3(scale, scale, scale);

			// parent the arrow to the PlayerCompass object
			arrowInstance.transform.parent = transform;

			// add the arrow to our list of arrows
			m_arrows.Add(arrowInstance);
        }
    }


	// use iTween to animate a single arrow
	void MoveArrow(GameObject arrowInstance)
    {
        // animate the arrow in a cycle from startDistance to endDistance in local z
		iTween.MoveBy(arrowInstance, iTween.Hash(
            "z", endDistance - startDistance,
            "looptype", iTween.LoopType.loop,
            "time", moveTime,
            "easetype", easeType
        ));
    }

	// animate all of the arrows
	void MoveArrows()
    {
        foreach (GameObject arrow in m_arrows)
        {
            MoveArrow(arrow);
        }
    }

    public void ShowArrows(bool state)
    {
        if (m_board == null)
        {
            Debug.LogWarning("PLAYERCOMPASS ShowArrows ERROR: no Board found!");
            return;
        }

        if (m_arrows == null || m_arrows.Count != Board.directions.Length)
		{
			Debug.LogWarning("PLAYERCOMPASS ShowArrows ERROR: no arrows found!");
			return;
		}

        if (m_board.PlayerNode != null)
        {
            for (int i = 0; i < Board.directions.Length; i++)
            {
                Node neighbor = m_board.PlayerNode.FindNeighborAt(Board.directions[i]);

                if (neighbor == null || !state)
                {
                    m_arrows[i].SetActive(false);
                }
                else
                {
                    bool activeState = m_board.PlayerNode.LinkedNodes.Contains(neighbor);
                    m_arrows[i].SetActive(activeState);
                }
            }
        }

        ResetArrows();
        MoveArrows();
    }

    void ResetArrows()
    {
        for (int i = 0; i < Board.directions.Length; i++)
        {
            iTween.Stop(m_arrows[i]);
            Vector3 dirVector = new Vector3(Board.directions[i].normalized.x, 0f,
                                            Board.directions[i].normalized.y);
            m_arrows[i].transform.position = transform.position + dirVector * startDistance;
        }
    }
}
