using System.Xml.Serialization;
using System;


public class LocalXML {

    [XmlAttribute("Local")]
    public string local;
    [XmlAttribute("LocalAnterior")]
    public string localAnterior;
    [XmlAttribute("Descricao")]
    public string descricao;
    [XmlAttribute("HoraEntrada")]
    public DateTime horaEntrada;
    [XmlAttribute("HoraSaida")]
    public DateTime horaSaida;
}
