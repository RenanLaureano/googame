using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemFreezer : MonoBehaviour, IDragHandler, IEndDragHandler
{

    public GainPrefab gainPrefab;
    public ItemDragDrop foodPrefab;
    public Transform GainsParent;
    public Image Icon;
    public Image Shadown;
    public Text TextQuantidade;

    private Comida comida;
    private PointerEventData eventData;
    private bool inDrag = false;
    private ItemDragDrop childInstantiate;

    public void Config(Comida comida, int quantidade)
    {
        this.comida = comida;
        this.TextQuantidade.text = "x" + quantidade.ToString();
        Init();
    }

    private void Init()
    {
        Shadown.sprite = Icon.sprite = comida.icon;

        foreach (Gain g in comida.gains)
        {
            GainPrefab tempGain = Instantiate(gainPrefab, GainsParent);
            tempGain.config(g);
        }
    }

    //public void OnClickUseItem()
    //{
    //    ItemDragDrop i = Instantiate(foodPrefab, GameObject.Find("CanvasDragDrop").transform,false);
    //    i.Config(()=> {
    //        Player.instance.OnItemFoodUp();
    //        GameController.instance.UseFood(comida);
    //        PopupFreezer.instance.Show();
    //    }, comida.icon);
    //    PopupFreezer.instance.Hidden();
        
    //}


    public void OnDrag(PointerEventData eventData)
    {
        if (inDrag && childInstantiate != null)
        {
            childInstantiate.OnDrag(eventData);
        }
        else
        {
            inDrag = true;

            ItemDragDrop i = Instantiate(foodPrefab, GameObject.Find("CanvasDragDrop").transform, false);

            i.Config(() =>
            {
                Player.instance.OnItemFoodUp(comida);
                GameController.instance.UseFood(comida);
                PopupFreezer.instance.Show();
            }, ItemDragDrop.TypeObject.food,comida.icon);

            i.transform.position = Input.mousePosition;

            childInstantiate = i;

            PopupFreezer.instance.Hidden();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (inDrag)
        {
            inDrag = false;
            childInstantiate.OnEndDrag(eventData);
            childInstantiate = null;
        }
    }
}
