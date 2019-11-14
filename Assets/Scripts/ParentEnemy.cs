using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentEnemy : MonoBehaviour
{
    public static int MAX_HEALTH = 30;

    public int health = MAX_HEALTH;
    public int damage = 1;
    public bool isGrounded = false;

    public float knockbackX = 300;
    public float knockbackY = 200;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Object.Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
            {
                collision.gameObject.GetComponent<PlayerController>().takeDamage(damage, 
                    new Vector2(-knockbackX, knockbackY));
            }
            else
            {
                collision.gameObject.GetComponent<PlayerController>().takeDamage(damage, 
                    new Vector2(knockbackX, knockbackY));
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
            {
                collision.gameObject.GetComponent<PlayerController>().takeDamage(damage,
                    new Vector2(-knockbackX, knockbackY));
            }
            else
            {
                collision.gameObject.GetComponent<PlayerController>().takeDamage(damage,
                    new Vector2(knockbackX, knockbackY));
            }
        }
    }
}
