 using UnityEngine;
 using System.Collections;
 
 public class geladeira : MonoBehaviour {
	

     SpriteRenderer spriteRenderer; //will store sprite renderer
	 public Sprite geladeiraAberta;
	 public Sprite geladeiraFechada;
     void Start()
     {
         spriteRenderer = gameObject.GetComponent<SpriteRenderer> (); //get sprite renderer & store it
         if (spriteRenderer.sprite == null) // if the sprite on spriteRenderer is null then
             spriteRenderer.sprite = geladeiraFechada; // set the sprite to sprite1
     }
 
     public void Clicked()
     {
		if (spriteRenderer.sprite == geladeiraAberta) {
			spriteRenderer.sprite = geladeiraFechada;
			Debug.Log("Fechou Geladeira");
		}else
		if (spriteRenderer.sprite == geladeiraFechada) {
			spriteRenderer.sprite = geladeiraAberta;
			Debug.Log("Abriu Geladeira");
		}
     }
 }