using UnityEngine;
using System;

public class SpikeTrap : Trap
{
    public override void Init()
    {
        base.Init();

        _collider.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            KillPlayer();
        }
    }
}