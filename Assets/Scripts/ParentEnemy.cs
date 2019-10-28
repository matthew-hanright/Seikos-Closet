using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentEnemy : MonoBehaviour
{
    public static int MAX_HEALTH = 30;

    public int health = MAX_HEALTH;
    public int damage = 1;
    public bool isGrounded = false;

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
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().takeDamage(damage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().takeDamage(damage);
            if (health <= 0)
                Object.Destroy(this.gameObject);
        }
    }
}
