using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	//Particle
	public GameObject fireExplosion;

	//Movement
	[HideInInspector]
	public float velocity;

	void FixedUpdate(){

		transform.position += transform.right * velocity;
		
		if (transform.position.x > 12 || transform.position.y > 8)
			Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag("Bird")){
			Instantiate(fireExplosion, transform.position, transform.rotation);
			other.gameObject.GetComponent<Scrollingobjects>().Kill(gameObject);
			PlayerController.playerController.AddScore(1);
			Destroy(gameObject);
		}
	}
}
