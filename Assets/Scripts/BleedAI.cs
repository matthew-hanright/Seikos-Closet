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
    private bool firstFall = false;
    private bool isStunned = false;

    public float jumpForce = 400f;
    private float stunnedTime;
    private float stunnedLength = 0.9f;

    public Sprite[] moving;
    private int currentFrame = 0;
    private float frameStartTime;
    public float frameRate = 0.12f;

    private float startTime;
    public float resetTime = 2f;
    private bool shouldReset = false;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        startingLocation = transform.position;
        GetComponent<SpriteRenderer>().enabled = false;
        frameStartTime = Time.time;
        GetComponent<SpriteRenderer>().sprite = moving[currentFrame];
    }

    void FixedUpdate()
    {
        if(isHiding)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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
            if(Time.time > frameStartTime + frameRate)
            {
                currentFrame++;
                if(currentFrame > moving.Length - 1)
                {
                    currentFrame = 0;
                }
                frameStartTime = Time.time;
                GetComponent<SpriteRenderer>().sprite = moving[currentFrame];
            }
            if (isGrounded)
            {
                currentFrame = 0;
                frameStartTime = Time.time;
                GetComponent<SpriteRenderer>().sprite = moving[currentFrame];
                GetComponent<Rigidbody2D>().AddForce(new Vector2(goalDirection.x * runSpeed, jumpForce));
            }
            /*if (!firstJump)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, jumpForce));
                firstJump = true;
            }*/
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
        if(shouldReset && Time.time > startTime + resetTime)
        {
            Reset();
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
            if (!firstFall)
            {
                isStunned = true;
                stunnedTime = Time.time;
                firstFall = true;
            }
        }
        if(collision.tag == "MainCamera")
        {
            shouldReset = false;
        }
    }

    public void OnPlayerEnter()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        isHiding = false;
        shouldReset = true;
        GetComponent<Rigidbody2D>().gravityScale = fallSpeed;
        startTime = Time.time;
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
            Reset();
        }
    }

    private void Reset()
    {
        isRunning = false;
        isHiding = true;
        isGrounded = false;
        GetComponentInChildren<AIGrounding>().groundingCollisions = 0;
        GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        GetComponent<SpriteRenderer>().enabled = false;
        transform.position = startingLocation;
        firstFall = false;
        isStunned = false;
        shouldReset = false;
        GetComponentInChildren<AIGrounding>().groundingCollisions = 0;
        GetComponentInChildren<BleedTriggerController>().restart();
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
