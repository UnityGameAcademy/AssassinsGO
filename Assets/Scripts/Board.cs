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

    // the Node directly under the Player
    Node m_playerNode;
    public Node PlayerNode { get { return m_playerNode; } }

    // the Node representing the end of the maze
    Node m_goalNode;
    public Node GoalNode { get { return m_goalNode; } }

 
    // iTween parameters for drawing the goal
    public GameObject goalPrefab;
    public float drawGoalTime = 2f;
    public float drawGoalDelay = 2f;
    public iTween.EaseType drawGoalEaseType = iTween.EaseType.easeOutExpo;

    // the PlayerMover component
    PlayerMover m_player;

    // transforms storing positions for captured enemies
    public List<Transform> capturePositions;

    // next index of capturePositions to use
    int m_currentCapturePosition = 0;
	public int CurrentCapturePosition { get { return m_currentCapturePosition; } 
        set { m_currentCapturePosition = value; } }

    // icon and color of each capturePosition
    public float capturePositionIconSize = 0.4f;
    public Color capturePositionIconColor = Color.blue;


	void Awake()
    {
        m_player = Object.FindObjectOfType<PlayerMover>().GetComponent<PlayerMover>();
        GetNodeList();

        m_goalNode = FindGoalNode();
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

    Node FindGoalNode()
    {
        return m_allNodes.Find(n => n.isLevelGoal);
    }

    // return the PlayerNode
    public Node FindPlayerNode()
    {
        if (m_player != null && !m_player.isMoving)
        {
            return FindNodeAt(m_player.transform.position);
        }
        return null;
    }

    public List<EnemyManager> FindEnemiesAt(Node node)
    {
        List<EnemyManager> foundEnemies = new List<EnemyManager>();
        EnemyManager[] enemies = Object.FindObjectsOfType<EnemyManager>() as EnemyManager[];

        foreach (EnemyManager enemy in enemies)
        {
            EnemyMover mover = enemy.GetComponent<EnemyMover>();

            if (mover.CurrentNode == node)
            {
                foundEnemies.Add(enemy);
            }
        }
        return foundEnemies;
    }

    // set the m_playerNode
    public void UpdatePlayerNode()
    {
        m_playerNode = FindPlayerNode();
    }

    // draw a colored sphere at the PlayerNode
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 1f, 1f, 0.5f);
        if (m_playerNode != null)
        {
            Gizmos.DrawSphere(m_playerNode.transform.position, 0.2f);
        }

        // draw capture positions
        Gizmos.color = capturePositionIconColor;

        foreach (Transform capturePos in capturePositions)
        {
            Gizmos.DrawCube(capturePos.position, Vector3.one * capturePositionIconSize);
        }
    }

    // draw the Goal prefab at the Goal Node
    public void DrawGoal()
    {
        if (goalPrefab != null && m_goalNode != null)
        {
            GameObject goalInstance = Instantiate(goalPrefab, m_goalNode.transform.position,
                                                  Quaternion.identity);
            iTween.ScaleFrom(goalInstance, iTween.Hash(
                "scale", Vector3.zero,
                "time", drawGoalTime,
                "delay", drawGoalDelay,
                "easetype", drawGoalEaseType
            ));
        }
    }

    // start initializing the Nodes/drawing links
    public void InitBoard()
    {
        if (m_playerNode != null)
        {
            m_playerNode.InitNode();
        }
    }


}
