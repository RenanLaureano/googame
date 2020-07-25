using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ItemGame : MonoBehaviour {

    public string nomeObjeto;
    public string descricaoObjeto;

    void Start() {
        GetComponent<Button>().onClick.AddListener(OnClickItem);
    }

    private void OnClickItem()
    {
        LogInteracao item = new LogInteracao(
            nomeObjeto,
            descricaoObjeto,
            GameController.instance.GetLocal(),
            System.DateTime.Now
        );

        GameController.instance.SaveComportamento(ComportamentosType.item,item);
    }

}
