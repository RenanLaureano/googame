using System.Collections.Generic;
using System.Xml.Serialization;

public class EventosEsperadosXML
{
    [XmlArray("Save_Eventos")]
    [XmlArrayItem("Eventos")]
    public List<EventoXML> eventos = new List<EventoXML>();
}
