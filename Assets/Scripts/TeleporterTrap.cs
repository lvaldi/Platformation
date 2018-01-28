using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterTrap : Trap {

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			Vector3 position = other.gameObject.transform.position;
			Vector3 newPosition = position + (Vector3.up * 5);
			other.gameObject.transform.position = newPosition;
		}
	}
}
