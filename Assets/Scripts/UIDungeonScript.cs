using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDungeonScript : MonoBehaviour
{
    public static Text healthDisplay;
    public static  Text shieldDisplay;

    private void Start()
    {
        PlayerController Player = FindObjectOfType<PlayerController>();
    }

    public void GrabHealth(int health)
    {
        healthDisplay.text = health.ToString();
    }

    public void GrabShield(int shield)
    {
        shieldDisplay.text = shield.ToString();
    }

}
