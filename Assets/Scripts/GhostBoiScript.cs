using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBoiScript : ParentEnemy
{
    //Sprite and frameRate Variables
    public Sprite[] ghostBoiFrames;
    private SpriteRenderer spriteRenderer;
    private int currentFrame = 0;
    private float frameRate;
    private float frameStartTime;
    private float frameRateMin = 0.1f; 
    private float frameRateMax = 0.7f;

    private bool onCamera = true;
    public bool seesPlayer = true;
    private bool closeEnough = false;
    public GameObject target;
    private float speed = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        frameStartTime = Time.time;
        frameRate = Random.Range(frameRateMin, frameRateMax);
        target = FindObjectOfType<PlayerController>().gameObject;
    }

    void FixedUpdate()
    {
        //update currentframe
        if (Time.time > frameStartTime + frameRate)
        {
            frameStartTime = Time.time;
            frameRate = Random.Range(frameRateMin, frameRateMax);
            currentFrame = Random.Range(0, ghostBoiFrames.Length);
            spriteRenderer.sprite = ghostBoiFrames[currentFrame];
        }

        //move towards the player character
        if (onCamera && seesPlayer && !closeEnough)
        {
            Vector2 posx = transform.position;
            transform.position = Vector2.MoveTowards(posx, target.transform.position, speed * Time.deltaTime);
            if (posx.x - transform.position.x < 0)
                spriteRenderer.flipX = true;
            else
                spriteRenderer.flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            closeEnough = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            closeEnough = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "MainCamera")
        {
            onCamera = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "MainCamera")
        {
            onCamera = false;
        }
    }
}
