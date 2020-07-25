using System;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class FadeInOut : MonoBehaviour
{
    public static FadeInOut instance = null;
    public UnityEngine.UI.Image bg;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        bg.color = new Vector4(0, 0, 0, 1);
        bg.gameObject.SetActive(true);
    }


    public void FadeIn(float duration, float delay = 0, Action onComplete = null)
    {
        StartCoroutine(ToFade(duration, 1, delay,onComplete));
    }

    public void FadeOut(float duration, float delay = 0, Action onComplete = null)
    {
        StartCoroutine(ToFade(duration, 0, delay, onComplete));
    }

    public void FadeToInToOut(float durationIn, float durationOut,float delay = 0, Action onComplete = null)
    {
        StartCoroutine(ToFade(durationIn, 1, delay,
            () =>
            {
                StartCoroutine(ToFade(durationOut, 0, delay, onComplete));
            }    
        ));
    }
    
    private IEnumerator ToFade(float duration,int a,float delay, Action onComplete = null)
    {
        bg.color = new Vector4(0, 0, 0, a == 0 ? 1 : 0);
        Color toColor = new Vector4(0, 0, 0, a);

        yield return new WaitForSeconds(delay);
        if(a == 1)
            bg.gameObject.SetActive(true);
        bg.DOColor(toColor, duration).OnComplete(
            () =>
            {
                if (onComplete != null)
                    onComplete();
                if (a != 1)
                    bg.gameObject.SetActive(false);
            }
        );
    }
}
