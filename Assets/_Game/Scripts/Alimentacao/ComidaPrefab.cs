using UnityEngine;
using UnityEngine.UI;

public class ComidaPrefab : MonoBehaviour {

    public Text nome;
    public Text preco;
    public Image icone;
    public Transform spawGains;
    public GainPrefab gainPrefab;
    public GameObject coinsInsuficiente;
    private Comida comida;
    private string tipo;
    public void Config(Comida comida, string tipo)
    {
        this.comida = comida;
        this.tipo = tipo;
        Init();
    }

    private void Init()
    {
        icone.sprite = comida.icon;
        nome.text = comida.name;
        preco.text = comida.value.ToString();

        foreach(Gain g in comida.gains)
        {
            GainPrefab tempGain = Instantiate(gainPrefab, spawGains);
            tempGain.config(g);
        }

        CheckCoins();

    }

    public void CheckCoins()
    {
        coinsInsuficiente.SetActive(!GameController.instance.CanBuy(comida.value));
        GetComponent<Button>().interactable = GameController.instance.CanBuy(comida.value);
    }

    public void CheckOthersButtons()
    {
        ComidaPrefab[] comidaPrefab = transform.parent.GetComponentsInChildren<ComidaPrefab>();
        foreach (ComidaPrefab cp in comidaPrefab)
            cp.CheckCoins();
    }
    public void OnClickBuyFood()
    {
        if(GameController.instance.GastarCoins(comida.value))
            GameController.instance.AddFood(comida,tipo);
        CheckOthersButtons();
    }
}
