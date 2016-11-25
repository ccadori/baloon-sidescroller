using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour {

	public float timeStopEmission;
	public float timeDestruct;

	void Awake(){

		Invoke("StopEmission", timeStopEmission);
		Invoke("Destruct", timeDestruct);
	}

	void StopEmission(){

		GetComponent<ParticleSystem>().enableEmission = false;
	}

	void Destruct(){

		Destroy(gameObject);
	}
}
