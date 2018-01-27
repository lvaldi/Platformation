using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    private Player player;
	public enum PLAYERS{
		Player1,
		Player2,
		Player3,
		Player4
	}

	public PLAYERS playerNumber;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
		playerControls ();
    }

	void playerControls() {
		int number = (int)playerNumber + 1;
		jumpAction ("Jump_" + number);
		directionalMovement ("Horizontal_" + number);
	}

	void directionalMovement(string horizontal) {
		Vector2 directionalInput = new Vector2(Input.GetAxisRaw(horizontal), Input.GetAxisRaw("Vertical"));
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
}
