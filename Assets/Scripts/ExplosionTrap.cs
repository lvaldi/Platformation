using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTrap : Trap {

	public override void Init()
	{
		base.Init();

		_collider.isTrigger = true;
	}
	
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
