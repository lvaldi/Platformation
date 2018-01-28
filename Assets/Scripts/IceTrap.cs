using UnityEngine;
using System;

public class IceTrap : Trap {
    public override void Init()
    {
        base.Init();

        _collider.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            GameController.instance.slowPlayer();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            GameController.instance.restore();
        }
    }
}