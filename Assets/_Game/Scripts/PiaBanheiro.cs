using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiaBanheiro : MonoBehaviour
{
    public GameObject prefabEffect;
    public Transform limitLeft;
    public Transform limitRight;
    public Transform limitUp;
    public Transform limitDown;

    public static PiaBanheiro Instance;
    private Coroutine effects;


    private void Awake()
    {
        if (PiaBanheiro.Instance == null)
            Instance = this;
    }


    private void OnEnable()
    {
        if (effects != null)
            StopCoroutine(effects);

        ShuffleSize[] shuffleSizes = GetComponentsInChildren<ShuffleSize>();

        foreach (ShuffleSize s in shuffleSizes)
        {
            if (s != null)
                Destroy(s.gameObject);
        }
        effects = StartCoroutine(SpanwEffects());
    }

    IEnumerator SpanwEffects()
    {
        float time = 0.1f;

        while (true)
        {
            time -= Time.deltaTime;

            if(time < 0)
            {
                time = 0.1f;
                float x = Random.Range(limitLeft.position.x, limitRight.position.x);
                float y = Random.Range(limitDown.position.y, limitUp.position.y);
                Instantiate(prefabEffect, new Vector3(x, y, 0), Quaternion.identity, transform);
            }
            yield return null;
        }
    }
}
