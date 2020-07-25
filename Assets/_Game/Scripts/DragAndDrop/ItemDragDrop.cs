using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;

public class ItemDragDrop : MonoBehaviour, IDragHandler, IEndDragHandler {

    public enum TypeObject
    {
        bath,
        food,
        toothbrush,
        medicamento,
    }

    private GameObject triggerObject;
    private bool canAction;
    private bool canEmit;
    public TypeObject type;
    private float time = -1;
    public GameObject emitPrefab;
    private Vector3 initialPos;
    private Transform initParent;
    public Transform spawnEmitPosition;
    private Action action;

    private void Start()
    {
        initialPos = transform.localPosition;
        initParent = transform.parent;
    }
    
    public void Config(Action action, TypeObject type,Sprite sprite = null)
    {
        this.action = action;
        this.type = type;
        if (sprite != null)
            GetComponent<Image>().sprite = sprite;
    }

    public void OnDrag(PointerEventData eventData)
    {
        canAction = false;
        canEmit = true;
        transform.SetParent(GameObject.Find("CanvasDragDrop").transform, false);
        transform.position = eventData.position;

        if (type == TypeObject.bath)
        {
            EffectsController.Instance.ActiveEffect(Effects.water_shower, true);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canAction = true;
        canEmit = false;
        transform.SetParent(initParent, false);
        transform.position = eventData.position;

        if (type == TypeObject.bath || type == TypeObject.toothbrush)
        {
            transform.DOLocalMove(initialPos, 0.5f);
            EffectsController.Instance.ActiveEffect(Effects.water_shower, false);

            LogAcao LogAcao = new LogAcao(
                type == TypeObject.bath ? "Tomar banho" : "Escovar os dentes",
                "",
                System.DateTime.Now,
                Medidores.Higiene
            );

            SaveGameController.Instance.AddComportamento(ComportamentosType.acao, LogAcao);
        }
        else if ((type == TypeObject.food || type == TypeObject.medicamento) && triggerObject == null)
        {
            canAction = false;

            if(type == TypeObject.food)
                PopupFreezer.instance.Show();
            else if (type == TypeObject.medicamento)
                PopupMedicamentos.instance.Show();

            transform.DOLocalMove(initialPos, 0.2f).OnComplete(() =>
            {
                Destroy(this.gameObject);
            });
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && (type == TypeObject.food || type == TypeObject.medicamento))
        {
            triggerObject = collision.gameObject;
            triggerObject.GetComponent<Player>().OnItemFoodEnter();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && (type == TypeObject.food || type == TypeObject.medicamento))
        {
            triggerObject.GetComponent<Player>().OnItemFoodExit();
            triggerObject = null;            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if ((type == TypeObject.food || type == TypeObject.medicamento) && canAction)
            {
                this.action();
                Destroy(gameObject);
            }
            else if (canEmit && type == TypeObject.bath && Time.fixedTime > time)
            {
                time = Time.fixedTime + 0.005f;
                GameObject t = Instantiate(emitPrefab, transform.position, transform.rotation,transform);
                t.transform.SetParent(t.transform.parent.parent);
                GameController.instance.AdicionarMedidor(Medidores.Higiene, 20 * Time.deltaTime);
            }            
        }

        if (collision.gameObject.tag == "Mouth" && canEmit && type == TypeObject.toothbrush && Time.fixedTime > time)
        {
            time = Time.fixedTime + 0.005f;
            Vector3 tempPos = transform.position;
            tempPos.x -= GetComponent<RectTransform>().sizeDelta.x - GetComponent<RectTransform>().sizeDelta.x/3;
            GameObject t = Instantiate(emitPrefab, spawnEmitPosition != null ? spawnEmitPosition.position : tempPos, transform.rotation, transform);
            t.transform.SetParent(t.transform.parent.parent);
            GameController.instance.CallAction(ActionGame.EscovarDente, 3 * Time.deltaTime);
        }
    }
}
