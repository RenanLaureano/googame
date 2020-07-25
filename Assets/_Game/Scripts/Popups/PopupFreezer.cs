using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class PopupFreezer : MonoBehaviour
{

    public Transform contentItens;
    public RectTransform popup;
    public ItemFreezer prefabItemFreezer;
    public UnityEngine.UI.Image background;
    public static PopupFreezer instance;
    public List<FreezerItens> foods = new List<FreezerItens>();
    private List<GameObject> instantiatesFoods = new List<GameObject>();
    private int currentIndex = 0;
    public GameObject noItens, haveItens;
    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        popup.anchoredPosition = new Vector2(0, -popup.sizeDelta.y);
        Show();
    }

    private void Init()
    {
        foods = GameController.instance.GetFoods();
        CountainItens(foods.Count > 0);
        if (foods.Count <= 0)
            return;

        haveItens.SetActive(!(foods.Count == 1));
        foreach (FreezerItens c in foods)
        {
            ItemFreezer tempComida = Instantiate(prefabItemFreezer, contentItens);
            tempComida.Config(c.comida, c.quantidade);
            tempComida.gameObject.SetActive(false);
            instantiatesFoods.Add(tempComida.gameObject);
        }
        instantiatesFoods[0].SetActive(true);
    }

    private void ResetItens()
    {
        foreach(GameObject i in instantiatesFoods)
            Destroy(i.gameObject);
        instantiatesFoods.Clear();
        Init();
    }

    public void Show()
    {
        ResetItens();
        popup.DOMoveY(0, 0.5f);
        AnimDefault.AnimIn(background);
    }

    public void Hidden()
    {
        popup.DOAnchorPosY(-popup.sizeDelta.y, 0.3f);
        AnimDefault.AnimOut(background);
    }

    public void Close()
    {
        popup.DOAnchorPosY(-popup.sizeDelta.y, 0.3f).OnComplete(() =>
        {
            GameController.instance.OnClickFridge();
            Destroy(this.gameObject);
        });
    }

    private void CountainItens(bool have)
    {
        if(noItens != null)
        {
            noItens.SetActive(!have);
        }

        if(haveItens != null)
        {
            haveItens.SetActive(have);
        }
    }

    public void OnClickShop()
    {
        Hidden();
        PrefabsController.instance.InstancePrefab(PrefabsController.instance.popupComidas);
    }

    public void OnClickNext()
    {
        currentIndex = currentIndex + 1 > instantiatesFoods.Count - 1 ? 0 : currentIndex + 1;
        instantiatesFoods[currentIndex].SetActive(true);

        int lastIndex = currentIndex == 0 ? instantiatesFoods.Count - 1 : currentIndex - 1;
        instantiatesFoods[lastIndex].SetActive(false);
    }

    public void OnClickBack()
    {
        currentIndex = currentIndex - 1 < 0 ? instantiatesFoods.Count - 1 : currentIndex - 1;
        instantiatesFoods[currentIndex].SetActive(true);

        int lastIndex = currentIndex == instantiatesFoods.Count - 1 ? 0 : currentIndex + 1;
        instantiatesFoods[lastIndex].SetActive(false);
    }

}

public class FreezerItens
{
   public Comida comida;
   public int quantidade;
}
