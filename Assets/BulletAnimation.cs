using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAnimation : MonoBehaviour {

	Vector3 startPoint;

	[SerializeField]
	Vector3 targetPoint;

	[SerializeField]
	float speed;
	float curScale = 0;

	[SerializeField]
	float targetScale;
	[SerializeField]
	float startScale;
	[SerializeField]
	float shrinkSpeed;
	[SerializeField]
	float crossHair;

	float timeTaken = 2.2f;
	float startTime;

	// Use this for initialization
	void Start () {
		startPoint = transform.position;
		transform.localScale = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		movePositon ();
		shrinkSize ();
		vanish ();
	}

	public void startShot(Vector3 location) {
		if (transform.position == targetPoint) {
			//Position
			transform.position = startPoint;
			targetPoint = location;

			transform.localScale = Vector3.one * startScale;
			startTime = Time.time;
		}

	}

	 
	void movePositon() {
		float step = (Time.time - startTime) / timeTaken;
		transform.position = Vector3.Lerp(transform.position, targetPoint, step);
	}

	void shrinkSize() {

		curScale = Mathf.Lerp(curScale, targetScale, Time.deltaTime * shrinkSpeed);
		transform.localScale = Vector3.one * curScale;

		transform.localScale -= Vector3.one*Time.deltaTime*shrinkSpeed;
	}

	void vanish() {
		if (withinDistance(targetPoint)) {
			transform.localScale = Vector3.zero;
		}
	}

	bool withinDistance (Vector3 targetPoint) {
		float distance = Vector3.Distance(targetPoint, transform.position);
		Debug.Log (distance);
		if (distance < 0.5) {
			return true;
		} else {
			return false;
		}

			
	}
}
