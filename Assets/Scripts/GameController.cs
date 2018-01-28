using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance;

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

	//Start position
	readonly Vector3 startPosition = new Vector3(-4.377336f,3.651274f,0f);

	//Referenced Gameobjects
	GameObject[] crosshairGOs;
    Crosshair[] crosshairs;
    GameObject player;


	public void Awake()
	{
		if(instance == null)
		{
            instance = this;
        }
		else if(instance != null)
		{
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

	// Use this for initialization
	void Start () {
		resetTimer ();
		mainPlayer = 1;
		mainPlayerinputControls = mainPlayerGameObject.GetComponent<PlayerInput> ();
		crosshairGOs = GameObject.FindGameObjectsWithTag ("Crosshair");
		player = GameObject.Find ("Player");

        crosshairs = new Crosshair[crosshairGOs.Length];
        for (int i = 0; i < crosshairGOs.Length; ++i)
		{
            crosshairs[i] = crosshairGOs[i].GetComponent<Crosshair>();
        }


        setupRound();
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


	public void endRound() {
		changeMainPlayer ();
		setupRound ();
		resetTimer ();
        SetupCrosshairs();
    }

	void SetupCrosshairs()
	{
		for (int i = 0; i < crosshairs.Length; ++i)
		{
            crosshairs[i].DesignatePlatformIndex();
        }
	}

	void setupRound() {
		setCrossHairActive ();
		respawnPlayer ();
	}


	void changeMainPlayer() {
		if (mainPlayer+1 > maxPlayers){
			mainPlayer = 1;
		} else {
			mainPlayer++;
		}

		mainPlayerinputControls.setCurrentPlayer (mainPlayer);
		Debug.Log ("Current Player Turn :" + mainPlayer);
	}

	void setCrossHairActive() {
		Debug.Log (mainPlayer + " Crosshair disabled");
		int i = -2;
		foreach (GameObject xhair in crosshairGOs) {
			if (xhair.name == "Crosshair_" + mainPlayer) {
				xhair.SetActive (false);
			} else {
				xhair.SetActive (true);
				xhair.GetComponent<Crosshair> ().DelayStart ();

				xhair.GetComponent<Crosshair> ().onPlayerKill += endRound;
			}
			xhair.transform.position = new Vector2 (i*5, 0);
			i++;
		}
	}

	void respawnPlayer() {
		player.transform.position = startPosition;
	}

	public void slowPlayer() {
		Player playerChange = mainPlayerGameObject.GetComponent<Player>();
		playerChange.moveSpeed = 2.0f;
    }

	public void restore() {
		Player playerChange = mainPlayerGameObject.GetComponent<Player>();
		playerChange.moveSpeed = 6.0f;
	}
}
