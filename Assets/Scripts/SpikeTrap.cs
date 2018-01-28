using UnityEngine;
using System;

public class SpikeTrap : Trap
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            KillPlayer();
        }
    }
}