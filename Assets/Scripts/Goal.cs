using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider2D))]
public class Goal : MonoBehaviour
{
    void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            GameController.instance.endRound();
			AudioController.instance.PLAY (AUDIO.GOAL);
        }
    }
}