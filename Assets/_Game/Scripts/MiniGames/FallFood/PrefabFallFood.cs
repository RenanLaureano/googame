using MyUtils;
using UnityEngine;
using UnityEngine.UI;

public class PrefabFallFood : MonoBehaviour
{
    public bool m_IsFood;
    public bool m_GoodItem;
    public Sprite m_IconCoin;
    public Sprite m_IconLife;

    [HideInInspector]
    public bool m_Coin;

    private RectTransform m_RectTransform;
    private float m_Speed;

    private void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector2 tempPos = m_RectTransform.anchoredPosition;
        tempPos.y = tempPos.y - m_Speed * Time.deltaTime;
        m_RectTransform.anchoredPosition = tempPos;
    }

    public void Config(ItemFallFood itemFallFood, float speedFall)
    {
        m_IsFood = true;
        GetComponent<Image>().sprite = itemFallFood.m_Icon;
        m_GoodItem = itemFallFood.m_GoodItem;
        m_Speed = speedFall;
    }

    public void Config(float speedFall)
    {
        m_GoodItem = true;
        m_IsFood = false;

        float prop = Random.Range(0, 100);
        m_Coin = prop <= 90;
        GetComponent<Image>().sprite = m_Coin ? m_IconCoin : m_IconLife;
        m_Speed = speedFall;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "ItemGame")
        {
            FallFoodController.Instance.AddToPoll(this);
            gameObject.SetActive(false);
        }
    }
}