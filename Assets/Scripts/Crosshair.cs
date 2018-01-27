using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour {

	public float speed = 0.5f;
	private float myTime = 0.0f;
	private float nextFire = 1.0f;
	private IEnumerator coroutine;
	private IEnumerator start;
	public 


	public void moveCrossHair(Vector2 input) {
		float x = transform.position.x + input.x*speed;
		float y = transform.position.y + input.y*speed;
		transform.position = new Vector3(x, y, 0);
	}

	private IEnumerator Delay(float waitTime) {
		yield return new WaitForSeconds(waitTime);
	}

	public void shoot() {
		coroutine = Delay(0.8f);
		StartCoroutine(coroutine);
		RaycastHit hit;
		if (Physics.Raycast (transform.position, Vector3.forward, out hit)) {
			if (hit.collider.tag == "Player") {
				transform();
			}
		}
	}

	public void fireRate() {

		coroutine = Delay(0.8f);
		myTime = myTime + Time.deltaTime;
		if(myTime >= nextFire) {
			shoot();
			myTime = 0.0f;
		}
	}

	private void delay() {
		delay = Delay(5.0f);
		StartCoroutine(delay);
	}

	public void transform() {
		GameObject obj = Instantiate(_platformPrefab, _transform);
        Platform platformComponent = obj.GetComponent<Platform>();
        platformComponent.Init();
	}
}
