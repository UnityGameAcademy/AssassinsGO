using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
	// (x,z) coordinate on Board, Coordinate property always returns rounded Vector
	Vector2 m_coordinate;
    public Vector2 Coordinate { get { return Utility.Vector2Round(m_coordinate); } }

	// list of nodes that are adjacent based on Board spacing
	List<Node> m_neighborNodes = new List<Node>();
    public List<Node> NeighborNodes { get { return m_neighborNodes; } }

	// reference to the Board component
	Board m_board;

    // reference to mesh for display of the node
    public GameObject geometry;

    // time for scale animation to play
    public float scaleTime = 0.3f;

    // ease in-out for animation
    public iTween.EaseType easeType = iTween.EaseType.easeInExpo;

    // do we activate the animation at Start?
    public bool autoRun = false;

    // delay time before animation
    public float delay = 1f;

    void Awake()
    {
		// find reference to the Board component
		m_board = Object.FindObjectOfType<Board>();

		// set the coordinate using the transform's x and z values
		m_coordinate = new Vector2(transform.position.x, transform.position.z);
    }

    void Start()
    {
        // hide the mesh by scaling to zero
        if (geometry != null)
        {
            geometry.transform.localScale = Vector3.zero;

            // play scale animation at Start
            if (autoRun)
            {
                ShowGeometry();
            }

			// find the neighboring nodes
			if (m_board != null)
            {
                m_neighborNodes = FindNeighbors(m_board.AllNodes);
            }
        }
    }

    // play scale animation
    public void ShowGeometry()
    {
        if (geometry != null)
        {
            iTween.ScaleTo(geometry, iTween.Hash(
                "time", scaleTime,
                "scale", Vector3.one,
                "easetype", easeType,
                "delay", delay
            ));
        }
    }

	// given a list of Nodes, return a subset of the list that are neighbors
	public List<Node> FindNeighbors(List<Node> nodes)
    {
		// temporary list of nodes to return
		List<Node> nList = new List<Node>();

		// loop through each of the Board directions
		foreach (Vector2 dir in Board.directions)
        {
			// find a neighboring node at the current direction...
			Node foundNeighbor = nodes.Find(n => n.Coordinate == Coordinate + dir);

			// if we find a neighbor at this direction, add it to the list
			if (foundNeighbor != null && !nList.Contains(foundNeighbor))
            {
                nList.Add(foundNeighbor);
            }
        }
		// return our temporary list
		return nList;
    }
}
