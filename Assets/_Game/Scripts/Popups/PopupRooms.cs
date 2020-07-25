using UnityEngine;
using DG.Tweening;

public class PopupRooms : MonoBehaviour {

    private UnityEngine.UI.Image background;
    private RectTransform popup;

    private void Start()
    {
        background = transform.GetChild(0).GetComponent<UnityEngine.UI.Image>();
        popup = background.transform.GetChild(0).GetComponent<RectTransform>();

        AnimDefault.AnimIn(background, popup);
    }

    public void OnClickClose()
    {

        AnimDefault.AnimOut(background, popup, gameObject);
    }

    public void OnClickSetRoom(int i)
    {
        GameController.instance.SetLocal((Locais)i);
        OnClickClose();
    }
}
