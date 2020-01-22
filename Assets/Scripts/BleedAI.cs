using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedAI : ParentEnemy
{
    //AI for the bleed enemy (ambush and run enemy)
    public int fallSpeed = 10;
    public GameObject player;
    public float runSpeed = 6f;

    private Vector3 startingLocation;
    private Vector2 goalDirection;

    private bool isHiding = true;
    private bool isRunning = false;
    private bool firstFall = true;
    private bool isStunned = false;

    public float jumpForce = 4f;
    private float stunnedTime;
    private float stunnedLength = 0.9f;

    public Sprite[] moving;
    private int currentFrame = 0;
    private float frameStartTime;
    public float frameRate = 0.12f;

    private float startTime;
    public float resetTime = 2f;
    private bool shouldReset = false;
    private bool isOnCamera = false;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        startingLocation = transform.position;
        //The bleed should be invisible until it is triggered
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
            //The bleed is stunned and temporarily unable to move 
            //if it hits the ground instead of the player
            if(Time.time >= stunnedTime + stunnedLength)
            {
                isStunned = false;
                isRunning = true;
            }
        }
        else if(isRunning)
        {
            //Animation
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

            //When on the ground, the bleed is constantly running and jumping
            if (isGrounded)
            {
                GetComponent<Rigidbody2D>().gravityScale = 12.0f;
                currentFrame = 0;
                frameStartTime = Time.time;
                GetComponent<SpriteRenderer>().sprite = moving[currentFrame];
                GetComponent<Rigidbody2D>().AddForce(new Vector2(goalDirection.x * runSpeed, jumpForce));
            }
            //The bleed should always run away from the player
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
        //If the bleed remains off-screen for too long, it resets
        //This is only encountered if the bleed hits an obstacle and never enters the screen
        if(shouldReset && Time.time > startTime + resetTime)
        {
            Reset();
        }

        //If the bleed has already been triggered and fallen, check if it has moved offscreen
        if (!isHiding && !firstFall)
        {
            CameraExit();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //As soon as the bleed hits the player it starts running away
        if (collision.tag == "Player")
        {
            isRunning = true;
            firstFall = false;
        }
        else if(collision.tag == "Solid")
        {
            //If the bleed hits the ground and not the player when it first falls down, it is stunned
            if (firstFall)
            {
                isStunned = true;
                stunnedTime = Time.time;
                firstFall = false;
            }
        }
        //If the bleed gets on-screen, it will reset when it exits and doesn't need the time-based reset
        if(collision.tag == "MainCamera")
        {
            shouldReset = false;
            isOnCamera = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "MainCamera")
        {
            isOnCamera = false;
        }
    }

    //This function is called when the player walks under the bleed, triggering it to attack
    public void OnPlayerEnter()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        isHiding = false;
        shouldReset = true;
        GetComponent<Rigidbody2D>().gravityScale = fallSpeed;
        startTime = Time.time;
    }

    //The bleed resets to its original position if it gets offscreen
    private void CameraExit()
    {
        Camera camera = Camera.main;
        if (!isOnCamera && 
            ((camera.ViewportToWorldPoint(new Vector3(0, 0.5f, transform.position.z)).x > 
                transform.position.x + transform.lossyScale.x * 15) ||
            (camera.ViewportToWorldPoint(new Vector3(1, 0.5f, transform.position.z)).x < 
                transform.position.x - transform.lossyScale.x * 15) ||
            (camera.ViewportToWorldPoint(new Vector3(0.5f, 0, transform.position.z)).y > 
                transform.position.y + transform.lossyScale.y * 15) ||
            (camera.ViewportToWorldPoint(new Vector3(0.5f, 1, transform.position.z)).y < 
                transform.position.y - transform.lossyScale.y * 15)))
        {
            Reset();
        }
    }

    //This function returns the bleed to its original position, and gets it ready to attack
    private void Reset()
    {
        isRunning = false;
        isHiding = true;
        isGrounded = false;
        GetComponentInChildren<AIGrounding>().groundingCollisions = 0;
        GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        GetComponent<SpriteRenderer>().enabled = false;
        transform.position = startingLocation;
        firstFall = true;
        isStunned = false;
        shouldReset = false;
        GetComponentInChildren<AIGrounding>().groundingCollisions = 0;
        GetComponentInChildren<BleedTriggerController>().restart();
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
