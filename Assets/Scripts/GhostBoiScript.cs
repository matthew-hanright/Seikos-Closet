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

    public bool seesPlayer = false;
    public GameObject target;
    private float speed = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        frameStartTime = Time.time;
        frameRate = Random.Range(frameRateMin, frameRateMax);
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
        if (target != null && seesPlayer)
        {
            Vector2 posx = transform.position;
            transform.position = Vector2.MoveTowards(posx, target.transform.position, speed * Time.deltaTime);
            if (posx.x - transform.position.x < 0)
                spriteRenderer.flipX = true;
            else
                spriteRenderer.flipX = false;
        }
    }
}
