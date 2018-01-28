using UnityEngine;

public enum PLAYERS{
	Player1,
	Player2,
	Player3,
	Player4
}

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
	private int playerNumber;
	public Player player;
	public PLAYERS currentPlayer;

    private void Start()
    {
		playerNumber = (int)currentPlayer + 1;
//		crosshair = GameObject.Find ("Crosshair_" + playerNumber).GetComponent<Crosshair>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
		playerControls ();
    }

	public void setCurrentPlayer(int p) {
		currentPlayer = (PLAYERS)p;
		playerNumber = p;
	}

	void playerControls() {
		jumpAction ("Jump_" + playerNumber);
		directionalMovement ("Horizontal_L_" + playerNumber);
//		crosshairMovement ("Horizontal_R_" + playerNumber,"Vertical_R_"+playerNumber);
//		shootingTrigger("Fire_"+playerNumber);
	}

	void directionalMovement(string horizontal) {
		Vector2 directionalInput = new Vector2(Input.GetAxisRaw(horizontal), 0);
		player.SetDirectionalInput(directionalInput);
	}

	void jumpAction(string action) {
		if (Input.GetButtonDown(action))
		{
			player.OnJumpInputDown();
		}

		if (Input.GetButtonUp(action))
		{
			player.OnJumpInputUp();
		}
	}

//	void shootingTrigger(string action){
//		if (Input.GetAxis (action) > 0 && !isTriggerDown) {
//            //TODO - Shoot button
//            crosshair.AttemptShot();
//            Debug.Log (Input.GetAxis (action));
//			isTriggerDown = true;
//		}
//
//		if (Input.GetAxis (action) <= 0) {
//			isTriggerDown = false;
//		}
//
//
//	}
//
//	void crosshairMovement(string horizontal, string vertical) {
//		Vector2 directionalInput = new Vector2(Input.GetAxisRaw(horizontal), Input.GetAxisRaw(vertical));
//		crosshair.moveCrossHair (directionalInput);
//	}
}
