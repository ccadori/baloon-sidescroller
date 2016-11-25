using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	//Combat
	public bool isAlive;
	public float maxHeight;
	public float minHeight;
	private float timeActualOutGame;
	public float timeMaxOutGame;

	//UI
	public Text HeightAlert;
	public Text scoreBoard;
	public Text recordScoreBoard;
	public GameObject LoosePanel;
	private int score;
	private float timeActualScore;
	public float timeToScore;
	public float timeToLoose;
	public bool canRestart;

	//Mesh
	public Transform baloon;
	public float maxScale;
	public float minScale;
	public float maxY;
	public float minY;
	public ParticleSystem fire;

	//Movement
	public float velocity;
	public float scaleGrow;

	//Components
	private Rigidbody2D rigidBody;
	public static PlayerController playerController;

	void Awake(){

		rigidBody = GetComponent<Rigidbody2D>();
		playerController = this;
	}

	void FixedUpdate(){

		if (Input.GetKey("escape"))
			Application.Quit();

		if (isAlive){

			timeActualScore += Time.deltaTime;

			if (timeActualScore > timeToScore){
				timeActualScore = 0;
				AddScore(1);
			}

			if (transform.position.y > maxHeight || transform.position.y < minHeight) {

				HeightAlert.text = "You are out of game scene (" + (int)(timeMaxOutGame - timeActualOutGame) + ")";
				timeActualOutGame += Time.deltaTime;
			} else {

				HeightAlert.text = "";
				timeActualOutGame = 0f;
			}

			if (timeActualOutGame > timeMaxOutGame)
				Kill();
		} else {

			if (canRestart){
			
				if (Input.GetKey("space"))
				    Application.LoadLevel("Gameplay");
			}
		}

		if (Input.GetKey("space") && isAlive){

			if (baloon.localScale.x < maxScale){

				baloon.localScale += new Vector3(scaleGrow,0,0);
			}
			fire.enableEmission = true;
			rigidBody.AddForce((Vector2.up * velocity) * Time.deltaTime);

		} else {

			if (baloon.localScale.x > minScale){
				
				baloon.localScale -= new Vector3(scaleGrow,0,0);
			}
			fire.enableEmission = false;
		}
	}

	public void Kill(){

		if (!isAlive)
			return;

		isAlive = false;
		minScale -= 0.15f;
		HeightAlert.text = "";

		CenaryController.cenaryController.SetPlayerStats();
		Invoke ("SetLooseUI", timeToLoose);

		Invoke ("DestroyCorpse", 5f);
	}

	public void SetLooseUI(){

		if (PlayerPrefs.GetInt("record") < score){

			PlayerPrefs.SetInt("record", score);
			recordScoreBoard.text = "New record: " + score.ToString();	
		
		} else {

			recordScoreBoard.text = "Your record: " + PlayerPrefs.GetInt("record").ToString();	
		}
	
		canRestart = true;
		LoosePanel.SetActive(true);
	}

	void DestroyCorpse (){

		rigidBody.gravityScale = 0f;
		rigidBody.velocity = new Vector2(0,0);
	}

	public void AddScore(int add){

		score += add;
		scoreBoard.text = score.ToString();
	}
}