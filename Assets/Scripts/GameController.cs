using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	[SerializeField]
	//Amount of time each round
	private float roundTime;
	public float timer { get; private set; }

	//Chosen player to be Main Player
	public int mainPlayer { get; private set; } 
	//Main Player Character game object
	public GameObject mainPlayerGameObject;
	PlayerInput mainPlayerinputControls;

	//Max amount of players allowed in game
	readonly int maxPlayers = 4;

	//Crosshair gameobjects
	GameObject[] crosshair;

	// Use this for initialization
	void Start () {
		resetTimer ();
		mainPlayer = 1;
		mainPlayerinputControls = mainPlayerGameObject.GetComponent<PlayerInput> ();
		crosshair = GameObject.FindGameObjectsWithTag ("Crosshair");
	}
	
	// Update is called once per frame
	void Update () {
		countDownTimer ();
	}


	void countDownTimer() {
		timer -= Time.deltaTime;
		if (timer < 0) {
			endRound ();
		}
	}

	void resetTimer() {
		timer = roundTime;
	}

	void endRound() {
		changeMainPlayer ();
		setCrossHairActive ();
		resetTimer ();
	}


	void changeMainPlayer() {
		mainPlayer++;
		if (mainPlayer > maxPlayers) {
			mainPlayer = 1;
		}

		mainPlayerinputControls.setCurrentPlayer (mainPlayer);
		Debug.Log ("Current Player Turn :" + mainPlayer);
	}

	void setCrossHairActive() {
		foreach (GameObject xhair in crosshair) {
			if (xhair.name == "Crosshair_" + mainPlayer) {
				xhair.SetActive (false);
			} else {
				xhair.SetActive (true);
			}
		}
	}
}
