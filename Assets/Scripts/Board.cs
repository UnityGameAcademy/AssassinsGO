using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    // uniform distance between nodes
    public static float spacing = 2f;

    // four compass directions
    public static readonly Vector2[] directions =
    {
        new Vector2(spacing, 0f),
        new Vector2(-spacing, 0f),
        new Vector2(0f, spacing),
        new Vector2(0f, -spacing)
    };

    // list of all of the Nodes on the Board
    List<Node> m_allNodes = new List<Node>();
    public List<Node> AllNodes { get { return m_allNodes; } }

    void Awake()
    {
        GetNodeList();
    }

    // sets the AllNodes and m_allNodes fields
    public void GetNodeList()
    {
        Node[] nList = Object.FindObjectsOfType<Node>();
        m_allNodes = new List<Node>(nList);
    }

    // returns a Node at a given position
    public Node FindNodeAt(Vector3 pos)
    {
        Vector2 boardCoord = Utility.Vector2Round(new Vector2(pos.x, pos.z));
        return m_allNodes.Find(n => n.Coordinate == boardCoord);

    }
}
