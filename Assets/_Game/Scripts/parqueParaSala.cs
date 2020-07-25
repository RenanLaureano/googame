using UnityEngine;
using System.Collections;

public class parqueParaSala : MonoBehaviour
{
	public Sprite backgroundSala;
	public Sprite backgroundParque;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	public void Clicked()
	{
		if ((GameObject.Find("local").GetComponent<SpriteRenderer>().sprite) == backgroundParque) 
		{
			GameObject.Find("parque_voltar").GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find("parque_voltar").GetComponent<BoxCollider2D>().enabled = false;
			GameObject.Find("parque_brincar").GetComponent<SpriteRenderer>().enabled = false;
			GameObject.Find("parque_brincar").GetComponent<BoxCollider2D>().enabled = false;
			GameObject.Find("local").GetComponent<SpriteRenderer>().sprite = backgroundSala;
			GameObject.Find("sala_porta").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("sala_porta").GetComponent<BoxCollider2D>().enabled = true;
			GameObject.Find("sala_sofa").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("sala_janela").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("sala_quadro").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("sala_tapete").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("setaDireita").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("setaDireita").GetComponent<BoxCollider2D>().enabled = true;
			GameObject.Find("setaEsquerda").GetComponent<SpriteRenderer>().enabled = true;
			GameObject.Find("setaEsquerda").GetComponent<BoxCollider2D>().enabled = true;
			Debug.Log("Foi para Sala");
		}
		else
		{

		}
	}

}

