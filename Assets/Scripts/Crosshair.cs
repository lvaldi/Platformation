using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour {

	public void moveCrossHair(Vector2 input) {
		float x = transform.position.x + input.x;
		float y = transform.position.y + input.y;
		transform.position = new Vector3(x, y, 0);
	}
}
