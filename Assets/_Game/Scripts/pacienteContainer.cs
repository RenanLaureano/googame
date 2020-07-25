using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class pacienteContainer
{
	[XmlArray("Paciente")]
	[XmlArrayItem("Paciente")]
	public List<paciente> pacientes = new List<paciente>();
}

