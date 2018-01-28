using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	public enum PlayerState
	{
		MOVING,
		SHOOTING
	}

	public enum PLAYERS{
		Player1,
		Player2,
		Player3,
		Player4
	}

	private Crosshair crosshair;
	private int playerNumber;
	private bool isTriggerDown;
	[SerializeField]
	private GameObject _playerGO;
    private Player _player;
    [SerializeField]
    private GameObject _crosshairGO;
    public PLAYERS currentPlayer;
    private PlayerState _currentState;

    private void Start()
    {
		isTriggerDown = false;
		playerNumber = (int)currentPlayer + 1;
		crosshair = _crosshairGO.GetComponent<Crosshair>();
        _player = _playerGO.GetComponent<Player>();
    }

    private void Update()
    {
		playerControls ();
    }

	public void setCurrentPlayer(int p) {
		currentPlayer = (PLAYERS)p;
		playerNumber = (int)p + 1;
	}

	public void SetCurrentPlayerState(PlayerState state)
	{
        _currentState = state;

		switch(_currentState)
		{
			case PlayerState.MOVING:
                _playerGO.SetActive(true);
                _crosshairGO.SetActive(false);
                break;
            case PlayerState.SHOOTING:
                _playerGO.SetActive(false);
                _crosshairGO.SetActive(true);
                break;
        }
    }

	void playerControls() {
		switch(_currentState)
		{
			case PlayerState.MOVING:
				jumpAction ("Jump_" + playerNumber);
				directionalMovement ("Horizontal_L_" + playerNumber);
                break;
			case PlayerState.SHOOTING:
				crosshairMovement ("Horizontal_R_" + playerNumber,"Vertical_R_"+playerNumber);
				shootingTrigger("Fire_"+playerNumber);
                break;
        }
		

	}

	void directionalMovement(string horizontal) {
		Vector2 directionalInput = new Vector2(Input.GetAxisRaw(horizontal), 0);
		_player.SetDirectionalInput(directionalInput);
	}

	void jumpAction(string action) {
		if (Input.GetButtonDown(action))
		{
			_player.OnJumpInputDown();
		}

		if (Input.GetButtonUp(action))
		{
			_player.OnJumpInputUp();
		}
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
