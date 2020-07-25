using UnityEngine;
using UnityEngine.UI;

public class PopupAlimentos : MonoBehaviour {

    [Header("Principal")]
    public GameObject popupPrincipal;

    [Header("Alimentos")]
    public GameObject popupAlimentos;
    public Text tipoComida;
    public ComidaPrefab comidaPrefab;
    public Transform contentComidas;

    private Image background;
    private RectTransform popup;
    private ScrollRect scrollComidas;

    void Start () {
        scrollComidas = popupAlimentos.transform.GetComponentInChildren<ScrollRect>();

        background = transform.GetChild(0).GetComponent<UnityEngine.UI.Image>();
        popup = background.transform.GetChild(0).GetComponent<RectTransform>();

        AnimDefault.AnimIn(background, popup);

        popupPrincipal.SetActive(true);
        popupAlimentos.SetActive(false);
    }

    private void InitComidas(TipoComida comida)
    {
        scrollComidas.normalizedPosition = new Vector2(0, 1);
        tipoComida.text = comida.name;

        contentComidas.transform.Clear();
        foreach (Comida c in comida.comidas)
        {
            ComidaPrefab tempComida = Instantiate(comidaPrefab, contentComidas);
            tempComida.Config(c,comida.name);
        }

        popupPrincipal.SetActive(false);
        popupAlimentos.SetActive(true);
    }

    public void OnClickTipoComida(TipoComida comida)
    {
        InitComidas(comida);
    }

    public void OnClickClosePopup()
    {
        AnimDefault.AnimOut(background, popup,gameObject);
        PopupFreezer.instance.Show();
    }

    public void OnClickCloseFood()
    {
        popupPrincipal.SetActive(true);
        popupAlimentos.SetActive(false);
    }

}
