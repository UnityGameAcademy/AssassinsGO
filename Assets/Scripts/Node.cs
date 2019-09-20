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

    // list of nodes that we are already linked to
    List<Node> m_linkedNodes = new List<Node>();
    public List<Node> LinkedNodes { get { return m_linkedNodes; } }

    // reference to the Board component
    Board m_board;

    // reference to mesh for display of the node
    public GameObject geometry;

    public GameObject linkPrefab;

    // time for scale animation to play
    public float scaleTime = 0.3f;

    // ease in-out for animation
    public iTween.EaseType easeType = iTween.EaseType.easeInExpo;

    // delay time before animation
    public float delay = 1f;

    // whether the node has already been initialized
    bool m_isInitialized = false;

    public LayerMask obstacleLayer;

    public bool isLevelGoal = false;

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
            Node foundNeighbor = FindNeighborAt(nodes, dir);

            // if we find a neighbor at this direction, add it to the list
            if (foundNeighbor != null && !nList.Contains(foundNeighbor))
            {
                nList.Add(foundNeighbor);
            }
        }
        // return our temporary list
        return nList;
    }

    public Node FindNeighborAt(List<Node> nodes, Vector2 dir)
    {
        return nodes.Find(n => n.Coordinate == Coordinate + dir);
    }

    public Node FindNeighborAt(Vector2 dir)
    {
        return FindNeighborAt(NeighborNodes, dir);
    }


    public void InitNode()
    {
        // if the Node has not been activated yet...
        if (!m_isInitialized)
        {
            // show the geometry
            ShowGeometry();

            // trigger its neighbors to the do the same
            InitNeighbors();

            // set our initialization state to true
            m_isInitialized = true;
        }
    }

    void InitNeighbors()
    {
        StartCoroutine(InitNeighborsRoutine());
    }

    IEnumerator InitNeighborsRoutine()
    {
        // pause for a short delay
        yield return new WaitForSeconds(delay);

        // run InitNode on each neighboring node if they are not already linked
        foreach (Node n in m_neighborNodes)
        {
            if (!m_linkedNodes.Contains(n))
            {
                Obstacle obstacle = FindObstacle(n);
                if (obstacle == null)
                {
                    LinkNode(n);
                    n.InitNode();
                }
            }
        }
    }

    // draw a link from this Node to a target Node
    void LinkNode(Node targetNode)
    {
        if (linkPrefab != null)
        {
            // instantiate our prefab and parent to this Node
            GameObject linkInstance = Instantiate(linkPrefab, transform.position, Quaternion.identity);
            linkInstance.transform.parent = transform;

            // draw the link
            Link link = linkInstance.GetComponent<Link>();
            if (link != null)
            {
                link.DrawLink(transform.position, targetNode.transform.position);
            }

            // track what Nodes have been linked to other Nodes
            if (!m_linkedNodes.Contains(targetNode))
            {
                m_linkedNodes.Add(targetNode);
            }

            if (!targetNode.LinkedNodes.Contains(this))
            {
                targetNode.LinkedNodes.Add(this);
            }
        }
    }

    Obstacle FindObstacle(Node targetNode)
    {
        Vector3 checkDirection = targetNode.transform.position - transform.position;
        RaycastHit raycastHit;

        if (Physics.Raycast(transform.position, checkDirection, out raycastHit, Board.spacing + 0.1f,
                            obstacleLayer))
        {
            //Debug.Log("NODE FindObstacle: Hit an obstacle from " + this.name + " to " + targetNode.name);
            return raycastHit.collider.GetComponent<Obstacle>();
        }
        return null;
    }
}
