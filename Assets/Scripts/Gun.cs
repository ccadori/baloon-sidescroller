using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	//Components
	public Animator anim;

	//Objects
	public GameObject cannon;
	public GameObject bullet;

	//Time
	public Transform bulletPoint;
	public float shootDellay;
	public float shootVelocity;

	//States
	public bool isReloading;

	void Awake(){
		anim = GetComponent<Animator>();
	}

	void Update(){

		if (PlayerController.playerController.isAlive){

			Vector3 direction = Target.target.gameObject.transform.position - transform.position;
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; ;
			cannon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

			if (Input.GetMouseButton(00)){
				Shoot();
				anim.SetInteger("state", 1);
			} else {
				anim.SetInteger("state", 0);
			}
		}
	}

	void Shoot(){

		if (isReloading)
			return;
		isReloading = true;
		GameObject newBullet = Instantiate(bullet, bulletPoint.position, cannon.transform.rotation) as GameObject;
		newBullet.GetComponent<Bullet>().velocity = shootVelocity;

		Invoke("Reload", shootDellay);
	}

	void Reload(){

		isReloading = false;
	}
}
