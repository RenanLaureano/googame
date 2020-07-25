using System;
using System.Collections.Generic;
using System.Xml.Serialization;

public class SaveGameXML
{
    [XmlArray("SaveGame")]
    [XmlArrayItem("FoodItens")]
    public List<FoodItemXML> foods = new List<FoodItemXML>();
    [XmlEnum("Coins")]
    public int coins;
    [XmlEnum("Saude")]
    public float saude;
    [XmlEnum("Energia")]
    public float energia;
    [XmlEnum("Dormindo")]
    public bool dormindo;
    [XmlEnum("Alimentacao")]
    public float alimentacao;
    [XmlEnum("Diversao")]
    public float diversao;
    [XmlEnum("Higiene")]
    public float higiene;
    [XmlEnum("LastTimeOn")]
    public DateTime lastTimeOn;
}
