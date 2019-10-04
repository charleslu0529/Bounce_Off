// the music used in this game was taken from Konno Yukari - Tokimeki no Doukasen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {

	public static GameManagerScript instance = null;
	public GameObject Player;
	public GameObject Bubble;
	public float WaitTimeforSpawnBubbles = 1f;
	public Text CenterText;
	public Text TimerText;
	public Text IntructionText;
	public float EndScreenTime = 0.5f;
	private Coroutine Spawner;
	//public float CoroutineInterval = 1f;
	float lowLimitRnd = 2f;
	bool died = false;
	float randomMultiplier;
	bool isRunning = false;
	float timer;
	float spawnGapCurrent = 1f;
	float spawnGapMax = 1f;



	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}else if(instance != this)
		{
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(!isRunning){
			if(Input.GetKey("space"))
			{
				isRunning = true;
				Spawner = StartCoroutine(BubbleCoroutine());
				CenterText.text = "";
				IntructionText.text = "";
			}
		}

		if( isRunning ){
			if( !died){
				timer += Time.deltaTime;
			}
			
		}
		TimerText.text = (Mathf.Round(timer * 10f)/10f).ToString();

		if( Input.GetKey("escape")){
			Application.Quit();
		}

		if( died){
			CancelInvoke("spawnBubble");

			CenterText.text = "GAME OVER";
			CenterText.fontSize = 50;
			
			EndScreenTime -= Time.deltaTime;
			if(EndScreenTime < 0){
				if( Input.GetKey("space")){
					SceneManager.LoadScene( SceneManager.GetActiveScene().name );
				}
			}
			
		}
		if(timer > 20){
			lowLimitRnd = 0.5f;
		}

		spawnGapCurrent -= Time.deltaTime;
		if(!died){
			if(spawnGapCurrent < 0){
				Spawner = StartCoroutine(BubbleCoroutine());
				spawnGapCurrent = Random.Range(spawnGapMax - 0.01f,spawnGapMax);
				spawnGapMax = spawnGapCurrent;
			}
		}
		



		randomMultiplier = Random.Range(lowLimitRnd, lowLimitRnd * 1.1f);
		
	}

	void FixedUpdate(){

	}

	IEnumerator BubbleCoroutine(){

		if(isRunning){
			invokeBubble();
		}
		Debug.Log("Running Coroutine");
		yield return new WaitForSeconds(WaitTimeforSpawnBubbles * randomMultiplier);

		invokeBubble();
		//InvokeRepeating ("invokeBubble", 0,0.7f);
	}

	void invokeBubble(){
		Invoke ("spawnBubble", WaitTimeforSpawnBubbles * randomMultiplier);
	}

	void spawnBubble(){
		Instantiate(Bubble,new Vector3(Random.Range(9.1f,9.3f),Random.Range(-4.2f,-1.3f),0),Quaternion.identity);
	}

	public bool getIsGameRunning(){
		return isRunning;
	}

	public bool getDied(){
		return died;
	}

	public void setDied(){
		died = true;
	}
}
