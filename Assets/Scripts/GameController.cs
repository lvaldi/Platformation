using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	[SerializeField]
	private float roundTime;
	public float timer { get; private set; }

	public int mainPlayer { get; private set; } 
	readonly int maxPlayers = 4;

	// Use this for initialization
	void Start () {
		resetTimer ();
		mainPlayer = 1;
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
		resetTimer ();
	}


	void changeMainPlayer() {
		mainPlayer++;
		if (mainPlayer > maxPlayers) {
			mainPlayer = 1;
		}

		Debug.Log ("Current Player Turn :" + mainPlayer);
	}
}
