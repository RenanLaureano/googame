using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class TimeGame : MonoBehaviour {
    

    public Text txTime;
    public static TimeGame instance;

    private string oldTime;
    private float cont;

    private void Awake()
    {
        UpdateTimeControlled();
        instance = this;
    }

    // Update is called once per frame
    void Update () {
        cont += Time.deltaTime;

        if (cont >= 1)
            UpdateTimeControlled();
        
    }

    void UpdateTimeControlled()
    {
        DateTime time = DateTime.Now;
        string minutos = time.Minute.ToString();
        txTime.text = time.Hour.ToString() + ":" + ((float.Parse(minutos)) < 10 ? "0" : "") + time.Minute.ToString();

        if (oldTime == txTime.text)
            return;

        oldTime = txTime.text;

        //if (EventosController.instance.eventosDeHoje != null && EventosController.instance.eventosDeHoje.Count > 0)
        //{

        //    foreach (EventoXML ev in EventosController.instance.eventosDeHoje)
        //    {
        //        if (ev.horario == txTime.text)
        //            ev.acao();
        //    }
        //}
        cont = 0;

    }
}
