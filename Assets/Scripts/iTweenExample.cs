using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iTweenExample : MonoBehaviour
{

    void Start()
    {
        iTween.RotateTo(gameObject, iTween.Hash(
            "y", 90f,
            "delay", 1f,
            "time", 2f,
            "easetype", iTween.EaseType.easeInOutExpo));
    }


}
