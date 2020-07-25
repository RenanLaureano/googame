using UnityEngine;

public class ShuffleSize : MonoBehaviour
{
    public float minScale;
    public float maxScale = 1;

    void Start()
    {
        float newSize = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(newSize, newSize, 1);
    }
}
