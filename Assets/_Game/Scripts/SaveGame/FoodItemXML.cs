using System.Xml.Serialization;

public class FoodItemXML
{
    [XmlAttribute("Tipo")]
    public string tipo;
    [XmlAttribute("Comida")]
    public string comida;
    [XmlAttribute("Quantidade")]
    public int quantidade;
}