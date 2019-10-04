using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleScript : MonoBehaviour {
	public Color RenColor = new Color(238f, 54f, 54f, 1);
	public Color ZetsuColor = new Color(255f,255f,255f,1);
	public float BubbleSpeed = 2f;
	public enum Nen {Ren, Zetsu}
	public Nen currentNen;
	// Use this for initialization
	void Start () {
		if(GameManagerScript.instance.getIsGameRunning())
		{
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(-2 * BubbleSpeed,0,0);	
		}

		float determineNen =  Random.Range(0,2f);
		if(determineNen < 1){
			currentNen = Nen.Zetsu;
		}else{
			currentNen = Nen.Ren;
		}

		if( currentNen == Nen.Ren){
			gameObject.GetComponent<SpriteRenderer>().color = RenColor;
		}else if( currentNen == Nen.Zetsu){
			gameObject.GetComponent<SpriteRenderer>().color = ZetsuColor;
		}

	}
	
	// Update is called once per frame
	void Update () {
		if(GameManagerScript.instance.getIsGameRunning()){
			if(gameObject.GetComponent<Rigidbody2D>().velocity == Vector2.zero){
				gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(-2 * BubbleSpeed,0,0);	
			}
		}
		if( GameManagerScript.instance.getDied() ){
			gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		}
	}

	void OnBecameInvisible(){
		Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D col2D){
		if( col2D.gameObject.GetComponent<PlayerScript>() == null ){

		} else {
			if( (int) col2D.gameObject.GetComponent<PlayerScript>().myNen != (int) this.currentNen ){
				GameManagerScript.instance.setDied();
				Destroy(this.gameObject);
			}
		}
		if( col2D.gameObject.GetComponent<BubbleScript>() == null ){

		} else {
			if ( (int) col2D.gameObject.GetComponent<BubbleScript>().currentNen != (int) this.currentNen ){
				Destroy(this.gameObject);
			}
		}
		
	}
}
