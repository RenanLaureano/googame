using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System;
using UnityEngine;

public class voltaSala : MonoBehaviour {

	public Sprite backgroundSala; 
	public Sprite backgroundCozinha;
	public Sprite backgroundBanheiro;
	public Sprite backgroundFarmacia;
	public Sprite backgroundQuarto;
	private SpriteRenderer spriteRenderer;
	public static string dataPath = string.Empty;


	comportamentoContainer cc = new comportamentoContainer();

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>(); 
		if (spriteRenderer.sprite == null) 
			spriteRenderer.sprite = backgroundSala; 

		if (Application.platform == RuntimePlatform.Android) {
			dataPath = System.IO.Path.Combine (Application.persistentDataPath, "comportamento_voltaSala.xml");
			//Debug.Log(dataPath);

		}else{
			dataPath = "comportamento.xml";
			//Debug.Log(dataPath);

		}
	}

	void Update()
	{
		Screen.sleepTimeout = (int)0f;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		if (Input.GetKeyDown(KeyCode.Space)) // apenas para debug
		{
			Clicked(); // metodo chamado através do monitor de toque
		}
	}


	//Metodo principal, invocado pelo monitor de toque
	void Clicked()
	{
		if (backgroundSala == (GameObject.Find("local").GetComponent<SpriteRenderer>().sprite)) 
		{
			Debug.Log("Foi para Quarto");
			deSalaParaQuarto();
			comportamento c = new comportamento();
			c.descricao = "Foi para a sala";
			c.objeto = "Seta esquerda";
			c.hora = System.DateTime.Now;
			cc.comportamentos.Add (c);
			SaveXML (dataPath, cc);
		}else
			if (backgroundQuarto == (GameObject.Find("local").GetComponent<SpriteRenderer>().sprite)) 
			{
				Debug.Log("Foi para Farmacia");
				deQuartoParaFarmacia ();
				comportamento c = new comportamento();
				c.descricao = "Foi para a Farmacia";
				c.objeto = "Seta esquerda";
				c.hora = System.DateTime.Now;
				cc.comportamentos.Add (c);
				SaveXML (dataPath, cc);
			}else
				if (backgroundFarmacia == (GameObject.Find("local").GetComponent<SpriteRenderer>().sprite)) 
				{
					Debug.Log("Foi para Banheiro");
					deFarmaciaParaBanheiro ();
					comportamento c = new comportamento();
					c.descricao = "Foi para o banheiro";
					c.objeto = "Seta esquerda";
					c.hora = System.DateTime.Now;
					cc.comportamentos.Add (c);
					SaveXML (dataPath, cc);
				}else
					if (backgroundBanheiro == (GameObject.Find("local").GetComponent<SpriteRenderer>().sprite)) 
					{
						Debug.Log("Foi para Cozinha");
						deBanheiroParaCozinha ();
						comportamento c = new comportamento();
						c.descricao = "Foi para a cozinha";
						c.objeto = "Seta esquerda";
						c.hora = System.DateTime.Now;
						cc.comportamentos.Add (c);
						SaveXML (dataPath, cc);

					}else
						if (backgroundCozinha == (GameObject.Find("local").GetComponent<SpriteRenderer>().sprite)) 
						{
							Debug.Log("Foi para Sala");
							deCozinhaParaSala ();
							comportamento c = new comportamento();
							c.descricao = "Foi para a sala";
							c.objeto = "Seta esquerda";
							c.hora = System.DateTime.Now;
							cc.comportamentos.Add (c);
							SaveXML (dataPath, cc);
						}

	}


	//Metodos
	void deSalaParaQuarto()
	{
		if ((GameObject.Find("local").GetComponent<SpriteRenderer>().sprite) == backgroundSala) 
		{
			GameObject.Find("sala_porta").GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find("sala_sofa").GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find("sala_janela").GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find("sala_quadro").GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find("sala_tapete").GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find("local").GetComponent<SpriteRenderer>().sprite = backgroundQuarto;
			GameObject.Find("quarto_cama").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("quarto_janela").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("quarto_tapete").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("quarto_armario").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("quarto_armario").GetComponent<BoxCollider2D>().enabled = true;
		}
		else
		{

		}
	}

	void deQuartoParaFarmacia()
	{
		if ((GameObject.Find("local").GetComponent<SpriteRenderer>().sprite) == backgroundQuarto) 
		{
			GameObject.Find("quarto_cama").GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find("quarto_janela").GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find("quarto_tapete").GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find("quarto_armario").GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find("quarto_armario").GetComponent<BoxCollider2D>().enabled = false;
			GameObject.Find("local").GetComponent<SpriteRenderer>().sprite = backgroundFarmacia;
			GameObject.Find("farmacia_quadro").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("farmacia_ursinho").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("farmacia_maleta").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("farmacia_maleta").GetComponent<BoxCollider2D>().enabled = true;
		}
		else
		{

		}
	}
	void deFarmaciaParaBanheiro()
	{
		if ((GameObject.Find("local").GetComponent<SpriteRenderer>().sprite) == backgroundFarmacia) 
		{
			GameObject.Find("farmacia_quadro").GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find("farmacia_ursinho").GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find("farmacia_maleta").GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find("farmacia_maleta").GetComponent<BoxCollider2D>().enabled = false;
			GameObject.Find("local").GetComponent<SpriteRenderer>().sprite = backgroundBanheiro;			
			GameObject.Find("banheiro_chuveiro").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("banheiro_chuveiro").GetComponent<BoxCollider2D>().enabled = true;
			GameObject.Find("banheiro_Troninho").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("banheiro_Toalha").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("banheiro_prateleira").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("banheiro_pia").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("banheiro_sabonete").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("banheiro_sabonete").GetComponent<BoxCollider2D>().enabled = true;
			GameObject.Find("banheiro_escovaDente").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("banheiro_escovaDente").GetComponent<BoxCollider2D>().enabled = true;
			GameObject.Find("banheiro_fioDental").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("banheiro_fioDental").GetComponent<BoxCollider2D>().enabled = true;
			GameObject.Find("banheiro_protetorSolar").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("banheiro_protetorSolar").GetComponent<BoxCollider2D>().enabled = true;
		}
		else
		{

		}
	}
	void deBanheiroParaCozinha()
	{
		if ((GameObject.Find("local").GetComponent<SpriteRenderer>().sprite) == backgroundBanheiro) 
		{
			GameObject.Find("banheiro_chuveiro").GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find("banheiro_chuveiro").GetComponent<BoxCollider2D>().enabled = false;
			GameObject.Find("banheiro_Troninho").GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find("banheiro_Toalha").GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find("banheiro_prateleira").GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find("banheiro_pia").GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find("banheiro_sabonete").GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find("banheiro_sabonete").GetComponent<BoxCollider2D>().enabled = false;
			GameObject.Find("banheiro_escovaDente").GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find("banheiro_escovaDente").GetComponent<BoxCollider2D>().enabled = false;
			GameObject.Find("banheiro_fioDental").GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find("banheiro_fioDental").GetComponent<BoxCollider2D>().enabled = false;
			GameObject.Find("banheiro_protetorSolar").GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find("banheiro_protetorSolar").GetComponent<BoxCollider2D>().enabled = false;
			GameObject.Find("local").GetComponent<SpriteRenderer>().sprite = backgroundCozinha;
			GameObject.Find("cozinha_mesa").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("cozinha_geladeira").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("cozinha_geladeira").GetComponent<BoxCollider2D>().enabled = true;
		}
		else
		{

		}
	}
	void deCozinhaParaSala()
	{
		if ((GameObject.Find("local").GetComponent<SpriteRenderer>().sprite) == backgroundCozinha) 
		{
			GameObject.Find("cozinha_mesa").GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find("cozinha_geladeira").GetComponent<SpriteRenderer>().enabled = false;			
			GameObject.Find("cozinha_geladeira").GetComponent<BoxCollider2D>().enabled = false;
			GameObject.Find("local").GetComponent<SpriteRenderer>().sprite = backgroundSala;
			GameObject.Find("sala_porta").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("sala_sofa").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("sala_janela").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("sala_quadro").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("sala_tapete").GetComponent<SpriteRenderer>().enabled = true;
		}
		else
		{

		}
	}

	private static void SaveXML(string caminho, object obj)
	{
		XmlSerializer serializer = new XmlSerializer(obj.GetType());
		FileStream stream = new FileStream (caminho, FileMode.Create);
		serializer.Serialize(stream, obj);
		stream.Close();
	}

}
