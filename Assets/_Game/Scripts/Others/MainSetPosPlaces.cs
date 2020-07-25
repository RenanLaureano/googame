using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSetPosPlaces : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        float w = Screen.width;

        for (int x = 0; x < transform.childCount; x++)
        {
            transform.GetChild(x).gameObject.SetActive(true);
            RectTransform local = transform.GetChild(x).GetComponent<RectTransform>();
            Vector3 newPos = local.position;
            newPos.x = (w * x) + w/2;
            local.position = newPos;
        }
    }
}

