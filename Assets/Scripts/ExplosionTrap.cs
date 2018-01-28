using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTrap : Trap {
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Platform" )
		{
			Destroy (other.gameObject);
		}
	}


	void Start() {
		Invoke ("Death", 1f);
	}

}
