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
    private float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        frameStartTime = Time.time;
        frameRate = Random.Range(frameRateMin, frameRateMax);
    }

    // Update is called once per frame
    void Update()
    {
        //update currentframe
        if(Time.time > frameStartTime + frameRate)
        {
            frameStartTime = Time.time;
            frameRate = Random.Range(frameRateMin, frameRateMax);
            currentFrame = Random.Range(0, ghostBoiFrames.Length);
            spriteRenderer.sprite = ghostBoiFrames[currentFrame];
        }

        //move towards the player character
        if (target != null && seesPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            if (transform.position.x > 0)
                spriteRenderer.flipX = true;
            else
                spriteRenderer.flipX = false;
        }
    }
}
