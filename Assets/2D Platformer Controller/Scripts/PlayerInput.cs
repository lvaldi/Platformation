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
        Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        player.SetDirectionalInput(directionalInput);

		switch (playerNumber) {
		case PLAYERS.Player1:
			jumpControl ("Jump");
			break;
		case PLAYERS.Player2:
			jumpControl ("Jump_1");
			break;

		}

    }

	private void jumpControl(string action) {
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
