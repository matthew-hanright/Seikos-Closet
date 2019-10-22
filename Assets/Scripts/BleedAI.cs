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

    private float jumpForce = 40f;
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
            if (Mathf.Round(GetComponent<Rigidbody2D>().velocity.x) < goalDirection.x * runSpeed)
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
                GetComponent<Rigidbody2D>().gravityScale = 1.0f;
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
            isStunned = true;
            stunnedTime = Time.time;
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
        if (collision.gameObject.tag == "MainCamera")
        {
            isRunning = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            GetComponent<SpriteRenderer>().enabled = false;
            transform.position = startingLocation;
            firstJump = false;
            GetComponentInChildren<BleedTriggerController>().restart();
        }
    }
}
