using UnityEngine;
using System.Collections;

public class Scrollingobjects : MonoBehaviour {

	//Bird
	public bool isAlive;

	//Movement
	public float velocity;

	void FixedUpdate(){

		if (isAlive)
			transform.position += new Vector3( velocity, 0, 0);

		if (transform.position.x < -20)
			Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.CompareTag("Player"))
			Kill (other.gameObject);
	}

	public void Kill (GameObject other){

		GetComponent<Animator>().enabled = false;
		GetComponent<Rigidbody2D>().gravityScale = 1;

		if (other.gameObject.CompareTag("Player"))
			if (isAlive)
				other.GetComponent<PlayerController>().Kill();

		isAlive = false;

		Invoke ("DestroyCorpse", 1f);
	}

	void DestroyCorpse (){

		Destroy(gameObject);
	}
}
