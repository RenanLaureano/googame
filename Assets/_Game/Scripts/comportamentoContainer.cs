using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class comportamentoContainer
{
	[XmlArray("Log")]
	[XmlArrayItem("Comportamento")]
	public List<comportamento> comportamentos = new List<comportamento>();
}

