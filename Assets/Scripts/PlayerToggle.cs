using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToggle : MonoBehaviour
{
    public PlayerController player;
    public GameObject newTrackingPoint;
    public bool visible = true;

    // Update is called once per frame
    void Update()
    {
        if (!visible)
        {
            player.gameObject.SetActive(false);
            FindObjectOfType<CameraController>().player = newTrackingPoint;
        }
        else if (visible)
        {
            player.gameObject.SetActive(true);
            FindObjectOfType<CameraController>().player = player.gameObject;
        }
    }
}
