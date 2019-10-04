using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	public Color RenColor;
	public float LerpTime = 1;
	Color ZetsuColor;
	Color currentColor;
	public enum Nen {Ren, Zetsu}
	public Nen myNen;
	// Use this for initialization
	void Start () {
		myNen = Nen.Zetsu;
		ZetsuColor = gameObject.GetComponent<SpriteRenderer>().color;
	}
	
	// Update is called once per frame
	void Update (){

		currentColor = gameObject.GetComponent<SpriteRenderer>().color;

		if( !GameManagerScript.instance.getDied() ){
			if( Input.GetKey("space")){
				myNen = Nen.Ren;
			}else{
				myNen = Nen.Zetsu;
			}
		}
		

		if( myNen == Nen.Ren){
			gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(currentColor, RenColor, Mathf.PingPong(Time.time * LerpTime, 0.5f));
		}else if( myNen == Nen.Zetsu){
			gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(currentColor, ZetsuColor, Mathf.PingPong(Time.time * LerpTime, 0.5f));
		}
	}
}