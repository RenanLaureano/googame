using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour {
    public float timeToDelete = 0.5f;
    // Use this for initialization
    void Start () {
        StartCoroutine(Destroy());
	}

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(timeToDelete);
        Destroy(gameObject);
    }
}
