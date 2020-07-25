using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemMedicamento : MonoBehaviour, IDragHandler, IEndDragHandler
{

    public ItemDragDrop medicamentoPrefab;
    public Image Icon;
    public Text TextQuantidade;
    public Text TextNome;
    public Sprite[] IconeMedicamento;

    private MedicamentoObject medicamento;
    private PointerEventData eventData;
    private bool inDrag = false;
    private ItemDragDrop childInstantiate;

    public void Config(MedicamentoObject medicamento)
    {
        this.Icon.sprite = IconeMedicamento[medicamento.Icone];
        this.medicamento = medicamento;
        this.TextNome.text = medicamento.Nome.ToString();
        this.TextQuantidade.text = medicamento.Quantidade.ToString();
    }
  

    public void OnDrag(PointerEventData eventData)
    {
        if (inDrag && childInstantiate != null)
        {
            childInstantiate.OnDrag(eventData);
        }
        else
        {
            inDrag = true;

            ItemDragDrop i = Instantiate(medicamentoPrefab, GameObject.Find("CanvasDragDrop").transform, false);

            i.Config(() =>
            {
                Player.instance.OnItemMedicamentoUp(medicamento);
                PopupMedicamentos.instance.Show();
            }, ItemDragDrop.TypeObject.medicamento, IconeMedicamento[medicamento.Icone]);

            i.transform.position = Input.mousePosition;

            childInstantiate = i;

            PopupMedicamentos.instance.Hidden();
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
