using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class paciente {
		
		[XmlElement("Nome")]
		public string nome;
		[XmlAttribute("Nascimento")]
		public string nascimento;
		[XmlAttribute("Sexo")]
		public string sexo;
		[XmlAttribute("Moedas")]
		public int moedas;
		[XmlElement("Informacoes Adicionais")]
		public string info;


}


