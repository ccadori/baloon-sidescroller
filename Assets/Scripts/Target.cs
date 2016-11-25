using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

	public static Target target;
	public Sprite targetClick;
	public Sprite targetNormal;
	public SpriteRenderer spriteRenderer;

	void Awake(){

		Cursor.visible = false;
		spriteRenderer = GetComponent<SpriteRenderer>();
		target = this;
	}

	void Update(){

		Vector3 mousePosition = Input.mousePosition;
		transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 5));

		if (Input.GetMouseButton(00))
			spriteRenderer.sprite = targetClick;
		else if (Input.GetMouseButtonUp(00))
			spriteRenderer.sprite = targetNormal;
	}
}
