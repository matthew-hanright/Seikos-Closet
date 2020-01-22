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

    public bool hit = false;
    private float timeHit = 0.0f;
    private float timeInHit = 0.15f;
    Color RED = new Color(255,0,0);
    Color WHITE = new Color(255, 255, 255);

    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        //Destroy this Object if its health is zero
        if (health <= 0)
        {
            Object.Destroy(this.gameObject);
        }

        //Temporarily changes the sprite color to Red when hit by the Player
        if (hit)
        {
            GetComponent<SpriteRenderer>().color = RED;
            hit = false;
        }
        else if (Time.time > timeHit + timeInHit)
            GetComponent<SpriteRenderer>().color = WHITE;
        
    }
    
    //Called by Player attacks
    //Triggers changes to this object that occur when hit by an attack
    public void onHit()
    {
        timeHit = Time.time;
        hit = true;
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
