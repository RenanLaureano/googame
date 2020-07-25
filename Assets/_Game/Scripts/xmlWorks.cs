using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class xmlWorks : MonoBehaviour {


	public string nome = "Rodrigo";
	public string nascimento = "02/04/1988";
	public string sexo = "masculino";
	public int moedas = 10000;
	public string informacoes = "Info Adicionais";

	public static string dataPath = string.Empty;
	public comportamentoContainer cc = new comportamentoContainer();

	// Use this for initialization
	void Start () {
		
		if (Application.platform == RuntimePlatform.Android) {
			dataPath = System.IO.Path.Combine (Application.persistentDataPath, "pacientes.xml");
			//dataPath = "../pacientes.xml";
			Debug.Log(dataPath);
		
		}else{
			//dataPath = System.IO.Path.Combine (Application.persistentDataPath, "Reources/pacientes.xml");
			dataPath = "pacientes.xml";

			//dataPath = System.IO.Path.Combine (Application.persistentDataPath, "/pacientes.xml");
			Debug.Log(dataPath);

		}

		paciente p = new paciente();
		pacienteContainer e = new pacienteContainer();

		p.nome = nome;
		p.moedas = moedas;
		p.nascimento = nascimento;
		p.sexo = sexo;
		p.info = informacoes;

		e.pacientes.Add(p);
	
		SaveXML (dataPath, e);
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*

	// DADOS 


	Funcionando somente no windows T_T

	private static void SaveXML(string nomeArquivo, object obj)
	{
		XmlSerializer serializer = new XmlSerializer(obj.GetType());
		TextWriter writer = new StreamWriter(nomeArquivo);
		serializer.Serialize(writer, obj);
		writer.Close();
	}

	*/


	private static void SaveXML(string caminho, object obj)
	{
		XmlSerializer serializer = new XmlSerializer(obj.GetType());
		FileStream stream = new FileStream (caminho, FileMode.Create);
		serializer.Serialize(stream, obj);
		stream.Close();
	}


}
