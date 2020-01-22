using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGrounding : MonoBehaviour
{
    //This variable allows for multiple grounding collisions, otherwise anytime a collision ended the object
    //would be considered ungrounded
    public int groundingCollisions;

    //Things are considered grounded when their grounding box collides with a solid object, or an enemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Solid" || collision.tag == "Enemy")
        {
            GetComponentInParent<ParentEnemy>().isGrounded = true;
            GetComponentInParent<Rigidbody2D>().velocity = new Vector2(GetComponentInParent<Rigidbody2D>().velocity.x, 0.0f);
            groundingCollisions++; 
        }
    }

    //Objects remain grounded unless all grounding collisions end
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Solid" || collision.tag == "Enemy")
        {
            groundingCollisions--;
            if (groundingCollisions == 0)
            {
                GetComponentInParent<ParentEnemy>().isGrounded = false;
            }
        }
    }
}
