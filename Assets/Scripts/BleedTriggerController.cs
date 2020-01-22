using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedTriggerController : MonoBehaviour
{
    //This script detects when the player walks under a bleed enemy, and causes it to attack
    private Vector3 startLocation;

    private void Start()
    {
        startLocation = transform.position;
    }

    //Because the attack trigger is a child of the bleed, it moves when the bleed is reset
    //This ensures that the attack trigger ends up in the correct position
    public void restart()
    {
        transform.position = startLocation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GetComponentInParent<BleedAI>().OnPlayerEnter();
        }
    }
}
