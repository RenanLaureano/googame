using System;
using UnityEngine;

public static class Utils
{

    public static Transform Clear(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        return transform;
    }

    public static string ToLongString(this DateTime value)
    {
        return Math.Truncate((value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds).ToString();
    }
}
