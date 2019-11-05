using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGrounding : MonoBehaviour
{
    public int groundingCollisions;
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
        if(collision.tag == "Solid" || collision.tag == "Enemy")
        {
            GetComponentInParent<ParentEnemy>().isGrounded = true;
            GetComponentInParent<Rigidbody2D>().velocity = new Vector2(GetComponentInParent<Rigidbody2D>().velocity.x, 0.0f);
            groundingCollisions++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Solid")
        {
            groundingCollisions--;
            if (groundingCollisions == 0)
            {
                GetComponentInParent<ParentEnemy>().isGrounded = false;
            }
        }
    }
}
