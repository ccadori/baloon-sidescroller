using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	void Update(){
		if (Input.GetKey("space"))
			Application.LoadLevel("Gameplay");
		if (Input.GetKey("escape"))
			Application.Quit();	
	}
}
