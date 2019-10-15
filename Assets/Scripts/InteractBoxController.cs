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
        //If can't interact, meaning dialogue is ongoing, the interact button isn't being pressed
        //and the player has control, the player can begin interacting again
        //Prevents holding the interact button from skipping dialogue
        if (!canInteract && Input.GetAxis("Interact") <= 0.1 && GetComponentInParent<PlayerController>().canControl)
        {
            canInteract = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        //If can interact, is not currently interacting with anything, and the object can be interacted with
        if (canInteract && GetComponentInParent<PlayerController>().canControl && other.GetComponent<Interactable>())
        {
            if (Input.GetAxis("Interact") > 0.5) 
            {
                //Call the object's dialogue, and the player can no longer interact, i.e. begin another dialogue
                other.GetComponent<Interactable>().DisplayText(GetComponentInParent<BasicDialogue>());
                canInteract = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        
    }
}
