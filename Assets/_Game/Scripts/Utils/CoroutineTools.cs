using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoroutineTools
{

    public static Coroutine ActionAfter(this MonoBehaviour context,float time, System.Action action)
    {
        return context.StartCoroutine(EnumeratorCoroutine(time,action));
    }

    private static IEnumerator EnumeratorCoroutine(float time, System.Action action)
    {
        while(time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        action();
    }

    public static Coroutine ActionLoop(this MonoBehaviour context, float time, System.Action action)
    {
        return context.StartCoroutine(EnumeratorCoroutine2(time, action));
    }

    private static IEnumerator EnumeratorCoroutine2(float time, System.Action action)
    {
        float cont = time;
        while (true)
        {
            cont -= Time.deltaTime;
            if (cont <= 0)
            {
                action();
                cont = time;
            }
            yield return null;
        }
        
    }
}
