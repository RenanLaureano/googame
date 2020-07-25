using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarScale : MonoBehaviour {

    public bool height = true;
    private float initialScale = 1;


    public void SetScale(float initialValue, float value)
    {
        float s = ((initialScale * value) / initialValue);

        if (height)
            transform.localScale = new Vector3(1, s, 2);
        else
            transform.localScale = new Vector3(s, 1, 1);

    }
}
