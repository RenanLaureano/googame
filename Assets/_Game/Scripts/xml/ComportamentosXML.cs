using System.Collections.Generic;
using System.Xml.Serialization;

public class ComportamentosXML  {
    [XmlArray("Log_de_Comportamentos")]
    [XmlArrayItem("Clique_em_itens")]
    public List<ItemXML> itens = new List<ItemXML>();
    [XmlArrayItem("Locais")]
    public List<LocalXML> locais = new List<LocalXML>();
}
