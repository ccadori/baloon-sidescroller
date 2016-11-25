using UnityEngine;
using System.Collections;

public class CenaryController : MonoBehaviour {

	//Birds
	public GameObject bird;
	public float birdMaxSpeed;
	public float birdMinSpeed;
	public float birdTimeSummonPlayerDeath;
	public float birdTimeSummon;
	public int birdMaxSummon;
	public int birdMinSummon;
	public float secondaryBirdMaxDistance;
	public float birdStartPosition;
	public float birdTimeSummonDecrease;

	//Clouds
	public GameObject[] clouds;
	public float maxYCloud;
	public float minYCloud;
	public float cloudMaxSpeed;
	public float cloudMinSpeed;
	public float cloudTimeSummon;

	//Player state
	public bool PlayerAlive;

	//Components
	public static CenaryController cenaryController;

	void Awake(){

		cenaryController = this;

		InstanceCloud();
		InstanceBird();
	}

	void InstanceCloud (){

		GameObject cloud = clouds[(int)Random.Range(0,4)];
		Vector3 position = new Vector3( 12, Random.Range(minYCloud, maxYCloud), 0);

		GameObject newCloud = Instantiate (cloud, position, Quaternion.Euler(0,0,0)) as GameObject;
		newCloud.GetComponent<SpriteRenderer>().sortingOrder = (int) Random.Range(-2, 4);
		newCloud.GetComponent<Scrollingobjects>().velocity = Random.Range(cloudMinSpeed, cloudMaxSpeed);

		Invoke("InstanceCloud", cloudTimeSummon);
	}

	void InstanceBird (){

		Vector3 position = new Vector3( birdStartPosition, Random.Range(minYCloud, maxYCloud), 0);
		
		GameObject newBird = Instantiate (bird, position, Quaternion.Euler(0,0,0)) as GameObject;
		newBird.GetComponent<Scrollingobjects>().velocity = Random.Range(birdMinSpeed, birdMaxSpeed);

		int birdQuantity = Random.Range(birdMinSummon, birdMaxSummon);
		for (int i = 0; i < birdQuantity; i++){
			InstanceSecondaryBird (newBird);
		}

		if (birdTimeSummon > 0.5f && PlayerAlive)
			birdTimeSummon -= birdTimeSummonDecrease;

		Invoke("InstanceBird", birdTimeSummon);

	}

	public void SetPlayerStats (){

		PlayerAlive = false;
		birdTimeSummon = birdTimeSummonPlayerDeath;
	}

	void InstanceSecondaryBird (GameObject firstBird){

		Vector3 position = new Vector3( 12, Random.Range(firstBird.transform.position.y, firstBird.transform.position.y + secondaryBirdMaxDistance), 0);

		GameObject newBird = Instantiate (bird, position, Quaternion.Euler(0,0,0)) as GameObject;
		newBird.GetComponent<Scrollingobjects>().velocity = Random.Range(birdMinSpeed, birdMaxSpeed);
	}
}