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

    private float jumpForce = 70f;

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
        else if(isRunning)
        {
            if (Mathf.Round(GetComponent<Rigidbody2D>().velocity.x) == 0)
            {
                if (isGrounded)
                {
                    GetComponent<Rigidbody2D>().gravityScale = 1.0f;
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
        if(collision.tag == "Player")
        {
            GetComponent<Rigidbody2D>().gravityScale = 1.0f;
            GetComponent<SpriteRenderer>().enabled = true;
            isHiding = false;
            GetComponent<Rigidbody2D>().gravityScale = fallSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isRunning = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "MainCamera")
        {
            isRunning = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<SpriteRenderer>().enabled = false;
            transform.position = startingLocation;
        }
        else if(!isRunning && !isHiding && collision.tag == "Player")
        {
            isHiding = true;
        }
    }
}
