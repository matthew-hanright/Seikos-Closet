using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToggle : MonoBehaviour
{
    public PlayerController player;
    public bool visible = true;

    // Update is called once per frame
    void Update()
    {
        if (!visible)
        {
            player.gameObject.SetActive(false);
        }
        else if (visible)
        {
            player.gameObject.SetActive(true);
        }
    }
}
