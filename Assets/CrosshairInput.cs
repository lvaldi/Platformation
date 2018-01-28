using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairInput : MonoBehaviour {

	private int playerNumber;
	private bool isTriggerDown;
	private Crosshair crosshair;
	public PLAYERS currentPlayer;

	// Use this for initialization
	void Start () {
		playerNumber = (int)currentPlayer + 1;
		isTriggerDown = false;
		crosshair = GetComponent<Crosshair> ();
	}
	
	// Update is called once per frame
	void Update () {
		crosshairMovement ("Horizontal_R_" + playerNumber,"Vertical_R_"+playerNumber);
		shootingTrigger("Fire_"+playerNumber);
	}



	void shootingTrigger(string action){
		if (Input.GetAxis (action) > 0 && !isTriggerDown) {
			//TODO - Shoot button
			crosshair.AttemptShot();
			Debug.Log (Input.GetAxis (action));
			isTriggerDown = true;
		}

		if (Input.GetAxis (action) <= 0) {
			isTriggerDown = false;
		}


	}

	void crosshairMovement(string horizontal, string vertical) {
		Vector2 directionalInput = new Vector2(Input.GetAxisRaw(horizontal), Input.GetAxisRaw(vertical));
		crosshair.moveCrossHair (directionalInput);
	}
}
