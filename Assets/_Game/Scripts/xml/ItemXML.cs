using System.Xml.Serialization;
using System;

public class ItemXML {

    [XmlAttribute("Objeto")]
    public string objeto;
    [XmlAttribute("Descricao")]
    public string descricao;
    [XmlAttribute("Local")]
    public string local;
    [XmlAttribute("Hora")]
    public DateTime hora;
}
