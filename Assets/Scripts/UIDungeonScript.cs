using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDungeonScript : MonoBehaviour
{
    public Slider healthDisplay;
    public Slider shieldDisplay;

    private void Start()
    {
        PlayerController Player = FindObjectOfType<PlayerController>();
    }

    public void GrabHealth(int health)
    {
        healthDisplay.value = health;
    }

    public void GrabShield(int shield)
    {
        shieldDisplay.value = shield;
    }

}
