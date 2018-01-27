using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    private Player player;
	private Crosshair crosshair;

	public enum PLAYERS{
		Player1,
		Player2,
		Player3,
		Player4
	}

	public PLAYERS playerNumber;

    private void Start()
    {
		int number = (int)playerNumber + 1;
		crosshair = GameObject.Find ("Crosshair_" + number).GetComponent<Crosshair>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
		playerControls ();
    }

	void playerControls() {
		int number = (int)playerNumber + 1;
		jumpAction ("Jump_" + number);
		directionalMovement ("Horizontal_L_" + number);
		crosshairMovement ("Horizontal_R_" + number,"Vertical_R_"+number);
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

	void crosshairMovement(string horizontal, string vertical) {
		Vector2 directionalInput = new Vector2(Input.GetAxisRaw(horizontal), Input.GetAxisRaw(vertical));
		crosshair.moveCrossHair (directionalInput);
	}
}
