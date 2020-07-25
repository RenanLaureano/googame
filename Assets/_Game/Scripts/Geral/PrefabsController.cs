using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsController : MonoBehaviour {

    public GameObject popupRooms;
    public GameObject popupComidas;
    public GameObject popupFreezer;
    public GameObject popupMedicamentos;
    public GameObject popupGameOver;

    public static PrefabsController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public GameObject InstancePrefab(GameObject prefab)
    {
        Transform c = GameObject.Find("CanvasPopups").transform;
        return Instantiate(prefab, c);
    }
}
