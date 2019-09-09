using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBoxController : MonoBehaviour
{
    private bool canInteract = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Interact") <= 0.1)
        {
            canInteract = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (canInteract && other.GetComponent<Interactable>())
        {
            if (Input.GetAxis("Interact") > 0.5)
            {
                Debug.Log("High");
            }
            canInteract = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        
    }
}
