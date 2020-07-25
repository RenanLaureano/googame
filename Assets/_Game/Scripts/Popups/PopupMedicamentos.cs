using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class PopupMedicamentos : MonoBehaviour
{

    public Transform contentItens;
    public RectTransform popup;
    public ItemMedicamento prefabItemMedicamento;
    public UnityEngine.UI.Image background;
    public static PopupMedicamentos instance;
    public List<MedicamentoObject> medicamentos = new List<MedicamentoObject>();
    private List<GameObject> instantiatesMedicamentos = new List<GameObject>();
    private int currentIndex = 0;
    public GameObject haveItens;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        Init();
        popup.anchoredPosition = new Vector2(0, -popup.sizeDelta.y);
        Show();
    }

    private void Init()
    {
        medicamentos = GameController.instance.GetMedicamentos();
        CountainItens(medicamentos.Count > 0);
        if (medicamentos.Count <= 0)
            return;

        haveItens.SetActive(!(medicamentos.Count == 1));
        foreach (MedicamentoObject m in medicamentos)
        {
            ItemMedicamento tempComida = Instantiate(prefabItemMedicamento, contentItens);
            tempComida.Config(m);
            tempComida.gameObject.SetActive(false);
            instantiatesMedicamentos.Add(tempComida.gameObject);
        }
        instantiatesMedicamentos[0].SetActive(true);
    }
    
    public void Show()
    {
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
            Destroy(this.gameObject);
        });
    }

    private void CountainItens(bool have)
    {
        haveItens.SetActive(have);
    }

    public void OnClickShop()
    {
        Hidden();
        PrefabsController.instance.InstancePrefab(PrefabsController.instance.popupComidas);
    }

    public void OnClickNext()
    {
        currentIndex = currentIndex + 1 > instantiatesMedicamentos.Count - 1 ? 0 : currentIndex + 1;
        instantiatesMedicamentos[currentIndex].SetActive(true);

        int lastIndex = currentIndex == 0 ? instantiatesMedicamentos.Count - 1 : currentIndex - 1;
        instantiatesMedicamentos[lastIndex].SetActive(false);
    }

    public void OnClickBack()
    {
        currentIndex = currentIndex - 1 < 0 ? instantiatesMedicamentos.Count - 1 : currentIndex - 1;
        instantiatesMedicamentos[currentIndex].SetActive(true);

        int lastIndex = currentIndex == instantiatesMedicamentos.Count - 1 ? 0 : currentIndex + 1;
        instantiatesMedicamentos[lastIndex].SetActive(false);
    }

}
