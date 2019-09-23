using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{
    // local direction to search (defaults to 2 units in front of enemy)
    public Vector3 directionToSearch = new Vector3(0f, 0f, 2f);

    // Node where our sensor is looking for the Player
    Node m_nodeToSearch;

    // reference to Board component
    Board m_board;

    // have we found the Player?
    bool m_foundPlayer = false;
    public bool FoundPlayer { get { return m_foundPlayer; }}

    void Awake()
    {
        // find Board component
        m_board = Object.FindObjectOfType<Board>().GetComponent<Board>();
    }

    // check if the Player has moved into our sensor
    public void UpdateSensor()
    {
        // convert the local directionToSearch into a world space 3d position
        Vector3 worldSpacePositionToSearch = transform.TransformVector(directionToSearch) 
                                                      + transform.position;
        if (m_board != null)
        {
            // find the node at the world space position to search
            m_nodeToSearch = m_board.FindNodeAt(worldSpacePositionToSearch);

            // if the node to search is the PlayerNode, then we have found the Player
            if (m_nodeToSearch == m_board.PlayerNode)
            {
                m_foundPlayer = true;
            }
        }
    }

    // for testing only
    //void Update()
    //{
    //    UpdateSensor();
    //}
}
