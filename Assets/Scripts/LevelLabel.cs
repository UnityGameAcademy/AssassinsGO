using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LevelLabel : MonoBehaviour
{

    Text m_text;

    void Awake()
    {
        m_text = GetComponent<Text>();

        if (m_text != null)
        {
            m_text.text = SceneManager.GetActiveScene().name;
        }
    }

}
