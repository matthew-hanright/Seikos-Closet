using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedTriggerController : MonoBehaviour
{
    private Vector3 startLocation;

    private void Start()
    {
        startLocation = transform.position;
    }

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

    private void OnTriggerExit2D(Collider2D collision)
    {
        GetComponentInParent<BleedAI>().OnTExit(collision);
    }
}
