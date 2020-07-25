using UnityEngine;
using UnityEngine.UI;

public class PP : MonoBehaviour
{

    public Sprite[] sprites;

    private void Start()
    {
        GetComponent<Image>().sprite = sprites[Random.Range(0, sprites.Length)];
        RectTransform rc = GetComponent<RectTransform>();
        float s = Random.Range(80, 120);
        rc.sizeDelta = new Vector2(s,s);
    }

    public void OnClick()
    {
        GameController.instance.RemovePP(transform);
    }
}
