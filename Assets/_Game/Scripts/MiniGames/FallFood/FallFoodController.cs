using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallFoodController : MonoBehaviour
{
    [SerializeField]
    private Transform m_PrefabCanvas;
    [SerializeField]
    private float m_TimeMakeDifficult = 10f;

    [Header("Itens")]
    [SerializeField]
    private PrefabFallFood m_PrefabFallFood;
    [SerializeField]
    private ItemFallFood[] m_ItensFallFood;
    [SerializeField]
    private float m_TimeToSpawn = 0.3f;
    [SerializeField]
    private float m_GravityScale = 30;

    [Header("Spawns")]
    [SerializeField]
    private Transform m_ParentSpawns;

    [Header("Life")]
    [SerializeField]
    private Image[] m_ImageLifes;
    [SerializeField]
    private Sprite m_FullLife;
    [SerializeField]
    private Sprite m_EmptyLife;

    [Header("Coins")]
    [SerializeField]
    private Text m_TextCoins;

    [Header("Points")]
    [SerializeField]
    private Text m_TextPoints;


    private int m_Lifes = 3;
    private int m_Coins = 0;
    private int m_Points = 0;
    private float m_FloatPoints = 0;
    private Transform[] m_Spawns;
    private int m_LastSpawn = 0;
    private float m_Counter = 0;
    private float m_CounterDifficult = 0;
    private bool m_GameOver;
    public static FallFoodController Instance;
    private int MAX_POOL = 30;
    private int CURRENT_POOL = 0;
    private List<PrefabFallFood> m_PoolFallFoods = new List<PrefabFallFood>();
    public bool GameOver
    {
        get
        {
            return m_GameOver;
        }
    }

    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    void Start()
    {
        ChangeLife();
        m_Spawns = m_ParentSpawns.GetComponentsInChildren<Transform>();
        m_TextCoins.text = m_Coins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_GameOver)
            return;

        m_Counter += Time.deltaTime;
        m_CounterDifficult += Time.deltaTime;
        m_FloatPoints += Time.deltaTime;
        m_Points = (int)m_FloatPoints;
        m_TextPoints.text = m_Points.ToString();

        if (m_Counter >= m_TimeToSpawn)
        {
            int indexSpaw;
            do
            {
                indexSpaw = Random.Range(0, m_Spawns.Length);

            } while (m_LastSpawn == indexSpaw);

            m_LastSpawn = indexSpaw;
            Transform tempPos = m_Spawns[indexSpaw];

            int indexItem = Random.Range(0, m_ItensFallFood.Length);
            ItemFallFood tempItem = m_ItensFallFood[indexItem];

            float prop = Random.Range(0, 100);
            bool isFood = prop <= 85;

            if (isFood)
            {
                PrefabFallFood tempPrefab = null;
                if (CURRENT_POOL < 30) { 
                    tempPrefab = Instantiate(m_PrefabFallFood, tempPos.position, Quaternion.identity, m_PrefabCanvas);
                    CURRENT_POOL++;
                }
                else if (m_PoolFallFoods.Count > 0)
                {
                    tempPrefab = m_PoolFallFoods[0];
                    RemovePoll(tempPrefab);
                    tempPrefab.transform.position = tempPos.position;
                    tempPrefab.gameObject.SetActive(true);
                }

                if(tempPrefab != null)
                {
                    tempPrefab.Config(tempItem, m_GravityScale);
                }
            }
            else
            {
                PrefabFallFood tempPrefab = null;
                if (CURRENT_POOL < 30)
                {
                    tempPrefab = Instantiate(m_PrefabFallFood, tempPos.position, Quaternion.identity, m_PrefabCanvas);
                    CURRENT_POOL++;
                }
                else if (m_PoolFallFoods.Count > 0)
                {
                    tempPrefab = m_PoolFallFoods[0];
                    RemovePoll(tempPrefab);
                    tempPrefab.transform.position = tempPos.position;
                    tempPrefab.gameObject.SetActive(true);
                }

                if (tempPrefab != null)
                {
                    tempPrefab.Config(m_GravityScale);
                }
            }

            m_Counter = 0;
        }

        if (m_CounterDifficult >= m_TimeMakeDifficult)
        {
            if(m_TimeToSpawn > 0.12f)
            {
                m_TimeToSpawn -= 0.01f;
            }
            if(m_GravityScale < 400)
            {
                m_GravityScale += 5;
            }
            m_CounterDifficult = 0;
        }

        if (SaveGameController.Instance != null)
        {
            float dv = SaveGameController.Instance.dataGame.dataGame.diversao;
            dv += Time.deltaTime / 3;
            dv = dv > 100 ? 100 : dv;
            SaveGameController.Instance.dataGame.dataGame.diversao = dv;

            float hg = SaveGameController.Instance.dataGame.dataGame.higiene;
            hg -= Time.deltaTime / 10;
            hg = hg < 0 ? 0 : hg;
            SaveGameController.Instance.dataGame.dataGame.higiene = hg;
        }
    }

    public void ChangeLife()
    {
        for (int x = 0; x < 3; x++)
        {
            m_ImageLifes[x].sprite = x < m_Lifes ? m_FullLife : m_EmptyLife;
        }
    }

    public void CollideItem(PrefabFallFood item)
    {
        if (item.m_GoodItem)
        {
            if (!item.m_IsFood)
            {
                if (item.m_Coin)
                {
                    m_Coins++;
                    m_TextCoins.text = m_Coins.ToString();
                }
                else
                {
                    m_Lifes = m_Lifes + 1 >= 3 ? 3 : m_Lifes + 1;
                    ChangeLife();
                }
            }

            m_FloatPoints += 2;
        }
        else
        {
            m_Lifes--;
            ChangeLife();

            if (m_Lifes <= 0)
            {
                m_GameOver = true;
                if (PrefabsController.instance != null)
                {
                    GameObject tempPopup = PrefabsController.instance.InstancePrefab(PrefabsController.instance.popupGameOver);
                    tempPopup.GetComponent<PopupGameOver>().Config(m_Points, 0, m_Coins, "MiniGameFallFood");
                }
            }
        }
    }

    public void AddToPoll(PrefabFallFood prefabFallFood)
    {
        m_PoolFallFoods.Add(prefabFallFood);
    }

    public void RemovePoll(PrefabFallFood prefabFallFood)
    {
        m_PoolFallFoods.Remove(prefabFallFood);
    }
}


[System.Serializable]
public class ItemFallFood
{
    public Sprite m_Icon;
    public bool m_GoodItem;
}