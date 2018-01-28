using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterTrap : Trap {

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			other.gameObject.transform.GetComponent<Player> ().respawnPlayer ();
		}
	}
}
