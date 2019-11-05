using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedAI : ParentEnemy
{
    public int fallSpeed = 10;
    public GameObject player;
    public float runSpeed = 6f;

    private Vector3 startingLocation;
    private Vector2 goalDirection;

    private bool isHiding = true;
    private bool isRunning = false;
    private bool firstJump = false;
    private bool isStunned = false;

    public float jumpForce = 400f;
    private float stunnedTime;
    private float stunnedLength = 0.9f;

    // Start is called before the first frame update
    void Start()
    {
        startingLocation = transform.position;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    void FixedUpdate()
    {
        if(isHiding)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }
        else if(isStunned)
        {
            if(Time.time >= stunnedTime + stunnedLength)
            {
                isStunned = false;
                isRunning = true;
            }
        }
        else if(isRunning)
        {
            if (Mathf.Abs(Mathf.Round(GetComponent<Rigidbody2D>().velocity.x)) < runSpeed - 2.0f)
            {
                if (isGrounded)
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(goalDirection.x * runSpeed, jumpForce));
                }
            }
            if (!firstJump)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, jumpForce));
                firstJump = true;
            }
            if(player.transform.position.x < transform.position.x)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(runSpeed, GetComponent<Rigidbody2D>().velocity.y);
                goalDirection = Vector2.right;
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-runSpeed, GetComponent<Rigidbody2D>().velocity.y);
                goalDirection = Vector2.left;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isRunning = true;
        }
        else if(collision.tag == "Solid")
        {
            if (!firstJump)
            {
                isStunned = true;
                stunnedTime = Time.time;
                firstJump = true;
            }
        }
    }

    public void OnPlayerEnter()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        isHiding = false;
        GetComponent<Rigidbody2D>().gravityScale = fallSpeed;
    }

    public void OnTExit(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MainCamera" && (
            (collision.transform.position.x - collision.bounds.extents.x > 
                transform.position.x + transform.lossyScale.x) ||
            (collision.transform.position.x + collision.bounds.extents.x < 
                transform.position.x - transform.lossyScale.x) ||
            (collision.transform.position.y - collision.bounds.extents.y > 
                transform.position.y + transform.lossyScale.y) ||
            (collision.transform.position.y + collision.bounds.extents.y < 
                transform.position.y - transform.lossyScale.y)))
        {
            /*if(collision.transform.position.x - collision.bounds.extents.x >
                transform.position.x + transform.lossyScale.x)
            {
                print("Out left");
            }
            else if (collision.transform.position.x + collision.bounds.extents.x <
                transform.position.x - transform.lossyScale.x)
            {
                print("Out right");
            }
            else if (collision.transform.position.y - collision.bounds.extents.y >
                transform.position.y + transform.lossyScale.y)
            {
                print("Out down");
            }
            else if (collision.transform.position.y + collision.bounds.extents.y <
                transform.position.y - transform.lossyScale.y)
            {
                print("Out up");
            }*/
            isRunning = false;
            isHiding = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            GetComponent<SpriteRenderer>().enabled = false;
            transform.position = startingLocation;
            firstJump = false;
            isStunned = false;
            GetComponentInChildren<BleedTriggerController>().restart();
            GetComponentInChildren<AIGrounding>().groundingCollisions = 0;
        }
    }
}
