using System.Xml.Serialization;
using System;

public class EventoXML  {
    [XmlAttribute("Horario")]
    public string horario;
    [XmlAttribute("Dias")]
    public string dias;
    [XmlAttribute("Acao")]
    public Action acao;
    [XmlAttribute("AcaoDoUsuario")]
    public bool acaoDoUsuario;
}
