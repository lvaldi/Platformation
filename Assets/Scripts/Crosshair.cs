using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour {

	public float speed = 0.5f;
	private float myTime = 0.0f;
	private float nextFire = 1.0f;
	private IEnumerator coroutine;
	private IEnumerator start;


	public void moveCrossHair(Vector2 input) {
		float x = transform.position.x + input.x*speed;
		float y = transform.position.y + input.y*speed;
		transform.position = new Vector3(x, y, 0);
	}

	private IEnumerator Delay(float waitTime) {
		yield return new WaitForSeconds(waitTime);
	}

	void Start() {
		start = Delay(5.0f);
		StartCoroutine(start);
	}

	void Update() {

		coroutine = Delay(0.8f);
		myTime = myTime + Time.deltaTime;
		if(Input.GetButton("Space") && myTime > nextFire) {
			
			StartCoroutine(coroutine);
			Fire();
			myTime = 0.0f;
		}
	}

	public void Fire() {
    	RaycastHit hit;
		if (Physics.Raycast (transform.position, Vector3.forward, out hit)) {
			if(hit.collider.tag=="Player"){
				print("transform");
			}
		}
	}
}
