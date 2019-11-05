using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrounding : MonoBehaviour
{
    private int groundingCollisions = 0;
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
        if ((collision.tag == "Solid" && collision.gameObject.layer != 11) ||
            collision.tag == "Enemy")
        {
            GetComponentInParent<PlayerController>().isGrounded = true;
            GetComponentInParent<Rigidbody2D>().velocity = new Vector2(GetComponentInParent<Rigidbody2D>().velocity.x, 0.0f);
            groundingCollisions++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Solid" && collision.gameObject.layer != 11) || 
            collision.gameObject.tag == "Enemy")
        {
            groundingCollisions--;
            if (groundingCollisions == 0)
            {
                GetComponentInParent<PlayerController>().isGrounded = false;
            }
        }
    }
}
