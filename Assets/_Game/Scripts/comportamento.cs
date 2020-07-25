using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System;

public class comportamento {

	[XmlAttribute("Descricao")]
	public string descricao;
	[XmlAttribute("Objeto")]
	public string objeto;
	[XmlAttribute("Hora")]
	public DateTime hora;


}


