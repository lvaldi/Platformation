using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    //Referenced Gameobjects
	[SerializeField]
    private GameObject[] _crosshairGOs;
    Crosshair[] crosshairs;
    GameObject player;

    [Header("UI"), SerializeField]
    private TextMeshProUGUI _timerTextMesh;
    [SerializeField]
    private TextMeshProUGUI _currentPlayerTextMesh;
    [SerializeField]
    private float _fadeDuration = 2.0f;
    private bool _readyToFade;
    private float _startTime;

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
		
		player = GameObject.Find ("Climber");

        crosshairs = new Crosshair[_crosshairGOs.Length];
        for (int i = 0; i < _crosshairGOs.Length; ++i)
		{
            crosshairs[i] = _crosshairGOs[i].GetComponent<Crosshair>();
        }

        setupRound();
    }
	
	// Update is called once per frame
	void Update () {
		countDownTimer ();

        FadeCurrentPlayerText();
    }

	void countDownTimer() {
		timer -= Time.deltaTime;
		if (timer < 0) {
			endRound ();
		}

        int timerAsInt = (int)Mathf.Floor(timer);
        _timerTextMesh.text = "Timer: " + timerAsInt;
    }

	void resetTimer() {
		timer = roundTime;
	}


	public void endRound() {
		changeMainPlayer ();
		setupRound ();
		resetTimer ();
    }

	void SetupCrosshairs()
	{
		for (int i = 0; i < crosshairs.Length; ++i)
		{
            crosshairs[i].DesignatePlatformIndex();
            crosshairs[i].UpdateKillCount();
        }
	}

	void setupRound() {
		setCrossHairActive ();
		player.GetComponent<Player>().respawnPlayer ();
		
		SetupCrosshairs();
        UpdateCurrentPlayerUI();
    }
		
	void changeMainPlayer() {
		if (mainPlayer+1 > maxPlayers){
			mainPlayer = 1;
		} else {
			mainPlayer++;
		}

        UpdateCurrentPlayerUI();
        mainPlayerinputControls.setCurrentPlayer (mainPlayer);
	}

	private void UpdateCurrentPlayerUI()
	{
		_currentPlayerTextMesh.text = "Current Player: \n " + mainPlayer.ToString();
        _readyToFade = true;
		_startTime = Time.time;

        Color32 currentPlayerColor = _crosshairGOs[mainPlayer - 1].GetComponent<SpriteRenderer>().color;
        _currentPlayerTextMesh.faceColor = new Color32(currentPlayerColor.r, currentPlayerColor.g, currentPlayerColor.b, 255);
    }

	private void FadeCurrentPlayerText()
	{
        if(!_readyToFade)
            return;

        
        float currentTime = Time.time;
        if(currentTime <= _startTime + _fadeDuration)
		{
            float ratio = (currentTime - _startTime) / _fadeDuration;
			byte alpha = (byte)(255f - ratio * 255);
            _currentPlayerTextMesh.faceColor = new Color32(_currentPlayerTextMesh.faceColor.r, 
															_currentPlayerTextMesh.faceColor.g,
															_currentPlayerTextMesh.faceColor.b,
															alpha);
        }
		else
		{
			_currentPlayerTextMesh.faceColor = new Color32(_currentPlayerTextMesh.faceColor.r, 
															_currentPlayerTextMesh.faceColor.g,
															_currentPlayerTextMesh.faceColor.b,
															0);
			_readyToFade = false;
		}
	}

	void setCrossHairActive() {
		
		int i = -2;
		foreach (GameObject xhair in _crosshairGOs) {
			if (xhair.name == "Crosshair_" + mainPlayer) {
				xhair.SetActive (false);
			} else {
				xhair.SetActive (true);
                Crosshair script = xhair.GetComponent<Crosshair>();
                script.DelayStart ();
				script.DesignatePlatformIndex();
				script.onPlayerKill += endRound;
			}
			xhair.transform.position = new Vector2 (i*5, 0);
			i++;
		}
	}
}
