using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventosController : MonoBehaviour {

    //public List<EventoXML> eventosDeHoje;
    //public static EventosController instance;
    //private string todayName;

    //private void Awake()
    //{
    //    instance = this;
    //}

    //IEnumerator Start () {
    //    yield return new WaitForSeconds(1);
    //    NameToday();
    //    eventosDeHoje = new List<EventoXML>();

    //    foreach (EventoXML ev in SaveGameController.Instance.eventos.eventos)
    //    {
    //        string[] diasDoEvento = ev.dias.Split(';');
    //        if (EventoHoje(diasDoEvento) && ev.acao != null)
    //            eventosDeHoje.Add(ev);
    //    }
    //}

    //private bool EventoHoje(string[] dias)
    //{
    //    foreach(string d in dias)
    //    {
    //        if (d == todayName)
    //            return true;
    //    }
    //    return false;
    //}

    //private void NameToday()
    //{
    //    switch (DateTime.Now.DayOfWeek)
    //    {
    //        case DayOfWeek.Friday:
    //            todayName = "sex";
    //            break;
    //        case DayOfWeek.Monday:
    //            todayName = "seg";
    //            break;
    //        case DayOfWeek.Saturday:
    //            todayName = "sab";
    //            break;
    //        case DayOfWeek.Sunday:
    //            todayName = "dom";
    //            break;

    //        case DayOfWeek.Thursday:
    //            todayName = "qui";
    //            break;
    //        case DayOfWeek.Tuesday:
    //            todayName = "ter";
    //            break;
    //        case DayOfWeek.Wednesday:
    //            todayName = "qua";
    //            break;
    //    }
    //}
}
