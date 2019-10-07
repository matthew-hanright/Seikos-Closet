using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerBounds : MonoBehaviour
{
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        parent.GetComponent<CameraController>().OnTriggerEnter2D(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        print(collision.gameObject.tag);
        parent.GetComponent<CameraController>().OnTriggerExit2D(collision);
    }
}
