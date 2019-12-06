using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBVisionScript : MonoBehaviour
{
    public bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !triggered)
        {
            //print("It works");
            GetComponentInParent<GhostBoiScript>().seesPlayer = true;
            triggered = true;
            GetComponentInParent<GhostBoiScript>().target = collision.gameObject;
        }
    }
}
