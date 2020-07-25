using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

public class ControllerXML : MonoBehaviour
{

    public string dataPathComportamento = string.Empty;
    public string dataPathEventos = string.Empty;

    public ComportamentosXML comportamentos;
    public EventosEsperadosXML eventos;

    public static ControllerXML instance;

    private void Awake()
    {
        instance = this;

        if (Application.platform == RuntimePlatform.Android)
        {
            dataPathComportamento = Application.persistentDataPath + "/comportamento.xml";
            dataPathEventos = Application.persistentDataPath + "/eventos.xml";
        }
        else
        {
            dataPathComportamento = "comportamento.xml";
            dataPathEventos = "eventos.xml";
        }
        ReadXMLComportamentos();
        ReadXMLEventos();
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Adionar comportamento a lista de comportamentos, setar o tipo e o comportamento
    /// </summary>
    public void AddComportamento(ComportamentosType comportamentosType, object comportamento)
    {
        switch (comportamentosType)
        {
            case ComportamentosType.item:
                comportamentos.itens.Add((ItemXML)comportamento);
                break;
            case ComportamentosType.troca_tela:
                comportamentos.locais.Add((LocalXML)comportamento);
                break;
            default:
                Debug.LogError("TIPO NÃO ENCONTRADO!!!!");
                break;
        }
        SaveXML();
    }

    public void SaveXML()
    {

        XmlSerializer serializer = new XmlSerializer(comportamentos.GetType());
        FileStream stream = new FileStream(dataPathComportamento, FileMode.Create);
        serializer.Serialize(stream, comportamentos);
        stream.Close();
    }

    private void ReadXMLComportamentos()
    {

        try
        {
            ComportamentosXML _comportamentos = null;

            if (File.Exists(dataPathComportamento))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ComportamentosXML));
                StreamReader reader = new StreamReader(dataPathComportamento);
                _comportamentos = (ComportamentosXML)serializer.Deserialize(reader.BaseStream);
                reader.Close();
            }

            comportamentos = (_comportamentos != null) ? _comportamentos : InitializeComportamento();
        }
        catch (IOException e)
        {
            Debug.LogError(e);
            comportamentos = InitializeComportamento();
        }
    }

    private ComportamentosXML InitializeComportamento()
    {
        ComportamentosXML _comportamentos = new ComportamentosXML();
        _comportamentos.itens = new List<ItemXML>();
        _comportamentos.locais = new List<LocalXML>();
        return _comportamentos;
    }

    private void ReadXMLEventos()
    {

        try
        {
            EventosEsperadosXML _eventos = null;

            if (File.Exists(dataPathEventos))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(EventosEsperadosXML));
                StreamReader reader = new StreamReader(dataPathEventos);
                _eventos = (EventosEsperadosXML)serializer.Deserialize(reader.BaseStream);
                reader.Close();
            }

            eventos = (_eventos != null) ? _eventos : InitializeEventos();
        }
        catch (IOException e)
        {
            Debug.LogError(e);
            eventos = InitializeEventos();
        }

        EventoXML ev = new EventoXML();
        ev.horario = "16:48";
        ev.dias = "sab";
        ev.acao = () => { Debug.LogWarning("Sabado"); };
        eventos.eventos.Add(ev);

        EventoXML ev2 = new EventoXML();
        ev2.horario = "16:48";
        ev2.dias = "ter;qua";
        ev2.acao = () => { Debug.LogWarning("Terça e Quarta"); };
        eventos.eventos.Add(ev2);

        EventoXML ev3 = new EventoXML();
        ev3.horario = "16:48";
        ev3.dias = "dom;qua;qui;sab";
        ev3.acao = () => { Debug.LogWarning("Domingo, quarta, quinta, sabado"); };
        eventos.eventos.Add(ev3);
    }

    private EventosEsperadosXML InitializeEventos()
    {
        EventosEsperadosXML _eventos = new EventosEsperadosXML();
        _eventos.eventos = new List<EventoXML>();
        return _eventos;
    }


    public void SaveXMLEventos()
    {
        XmlSerializer serializer = new XmlSerializer(eventos.GetType());
        FileStream stream = new FileStream(dataPathEventos, FileMode.Create);
        serializer.Serialize(stream, eventos);
        stream.Close();
    }

}
