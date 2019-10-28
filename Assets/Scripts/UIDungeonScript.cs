using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDungeonScript : MonoBehaviour
{
    public Text healthDisplay;
    public Text shieldDisplay;

    private void Start()
    {
        PlayerController Player = FindObjectOfType<PlayerController>();
    }

    public void GrabHealth(int health)
    {
        healthDisplay.text = "Health: " + health;
    }

    public void GrabShield(int shield)
    {
        shieldDisplay.text = "Shield: " + shield;
    }

}
