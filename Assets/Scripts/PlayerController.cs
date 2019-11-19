﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    
    public static int MAX_HEALTH = 2;
    public static int MAX_SHIELD = 100;

    public GameObject player;
    public GameObject[] attacks = new GameObject[3];
    public float maxSpeed = 35;
    public float rateOfStopping = 8;
    public float takeOffSpeed = 35;
    public float maxJumpHeight = 80;
    public float gravity = 10;
    public float maxFallSpeed = -80;
    public bool canControl = true;
    public float invincibilityLength = 2;
    public float regenLength = 1f;
    public int regenAmount = 2;

    public bool lookingRight = true;

    public int health = MAX_HEALTH;
	public int shield = MAX_SHIELD;

    private bool isWalking = false;
    private bool isInvincible = false;
    private bool canRegen = true;
    public bool isGrounded = false;
    private bool isJumping = false;
    private Vector2 move;
    private float timeOfHit;
    private float timeOfRegen;

    public Sprite[] walking;
    private int currentFrame = 0;
    private float walkingFrameRate = 0.12f;
    private float frameStartTime;

    public Sprite[] attack1Seiko;
    public Sprite[] attack1Sprite;
    public Sprite[] attackUpSeiko;
    public Sprite[] jump;
    private float attackFrameRate = 0.12f;
    private float jumpFrameRate = 0.12f;
    private bool isAttacking = false;

    public float lookUpDistance = 5;
    public float lookDownDistance = 5;

    private float magicMeleeDistance = 8f;

    private UIDungeonScript UIDungeon;
	
    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        frameStartTime = Time.time;
        UIDungeon = FindObjectOfType<UIDungeonScript>();
        UIDungeon.GrabHealth(health);
        UIDungeon.GrabShield(shield);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= timeOfHit + invincibilityLength)
        {
            isInvincible = false;
        }

        if(Input.GetButtonDown("Escape") && Time.timeScale > 0)
        {
            Time.timeScale = 0;
        }
        else if(Input.GetButtonDown("Escape") && Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

        if(Input.GetButton("Regen"))
        {
            if(canRegen)
            {
                if (shield < MAX_SHIELD)
                {
                    timeOfRegen = Time.time;
                    shield += regenAmount;
                    canRegen = false;
                }
                else if(shield > MAX_SHIELD)
                {
                    shield = MAX_SHIELD;
                }
                UIDungeon.GrabShield(shield);
            }
            else
            {
                if(Time.time > timeOfRegen + regenLength)
                {
                    canRegen = true;
                }
            }
        }
    }

    private void OnGUI()
    {
        if (isWalking && canControl && !isAttacking)
        {
            if (Time.time >= frameStartTime + walkingFrameRate)
            {
                if (currentFrame < walking.Length - 1)
                {
                    currentFrame += 1;
                }
                else
                {
                    currentFrame = 0;
                }
                frameStartTime = Time.time;
                GetComponent<SpriteRenderer>().sprite = walking[currentFrame];
            }
        }
        else if (canControl && isAttacking)
        {
            if (Time.time > frameStartTime + attackFrameRate)
            {
                currentFrame++;
                frameStartTime = Time.time;
                if (Input.GetAxis("Look Up") > 0.1)
                {
                    if (currentFrame > attackUpSeiko.Length - 1)
                    {
                        currentFrame = 0;
                        isAttacking = false;
                        GetComponent<SpriteRenderer>().sprite = walking[currentFrame];
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().sprite = attackUpSeiko[currentFrame];
                    }
                }
                else
                {
                    if (currentFrame > attack1Seiko.Length - 1)
                    {
                        currentFrame = 0;
                        isAttacking = false;
                        GetComponent<SpriteRenderer>().sprite = walking[currentFrame];
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().sprite = attack1Seiko[currentFrame];
                    }
                }
            }
        }
        else if(canControl & isJumping)
        {
            if (Time.time > frameStartTime + jumpFrameRate)
            {
                currentFrame++;
                frameStartTime = Time.time;
                if (currentFrame > jump.Length - 1)
                {
                    currentFrame = 0;
                    GetComponent<SpriteRenderer>().sprite = walking[currentFrame];
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = jump[currentFrame];
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (canControl)
        {
            //Movement
            //Horizontal Movement
            move.x = Input.GetAxis("Horizontal");
            if (Mathf.Abs(move.x) < maxSpeed)
            {
                move.x = Input.GetAxis("Horizontal") * maxSpeed;
                isWalking = true;
            }

            if (move.x < 0)
            {
                if (transform.localScale.x > 0)
                {
                    lookingRight = false;
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 1.0f);
                    //Swap which side the interact box is on when the player changes which direction
                    //they are facing
                }
            }
            else if (move.x > 0)
            {
                if (transform.localScale.x < 0)
                {
                    lookingRight = true;
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 1.0f);
                    //Flip the interact box as well 
                }
            }
            else
            {
                isWalking = false;
            }

            //Vertical Movement
            if (isGrounded && move.y < 1.0f)
            {
                if (Input.GetAxis("Vertical") > 0)
                {
                    currentFrame = 0;
                    frameStartTime = Time.time;
                    GetComponent<SpriteRenderer>().sprite = jump[currentFrame];
                    isJumping = true;
                    move.y = takeOffSpeed;
                }
                else
                {
                    isJumping = false;
                }
            }
            else if (isJumping)
            {
                if (move.y < maxJumpHeight)
                {
                    move.y += takeOffSpeed;
                    if(move.y > maxJumpHeight)
                    {
                        move.y = maxJumpHeight;
                    }
                    
                }
                else
                {
                    isJumping = false;
                }
            }
            else
            {
                if(move.y > -maxJumpHeight / 1.2f)
                {
                    move.y -= gravity;
                }
            }

            //Attack 1
            if (Input.GetButtonDown("Fire1") && !isAttacking)
            {
                GameObject combo = Instantiate(attacks[0]) as GameObject;
                if (Input.GetAxis("Look Up") > 0.1)
                {
                    combo.transform.position = new Vector2(
                        transform.position.x,
                        transform.position.y + (GetComponent<CapsuleCollider2D>().size.y / 2));
                    if (lookingRight)
                    {
                        combo.transform.localScale = new Vector2(Mathf.Abs(combo.transform.localScale.x), combo.transform.localScale.y);
                    }
                    else
                    {
                        combo.transform.localScale = new Vector2(-Mathf.Abs(combo.transform.localScale.x), combo.transform.localScale.y);
                    }
                }
                else if(Input.GetAxis("Look Down") > 0.1)
                {
                    combo.transform.position = new Vector2(
                        transform.position.x,
                        transform.position.y - (GetComponent<CapsuleCollider2D>().size.y / 2));
                    if (lookingRight)
                    {
                        combo.transform.localScale = new Vector2(Mathf.Abs(combo.transform.localScale.x), combo.transform.localScale.y);
                    }
                    else
                    {
                        combo.transform.localScale = new Vector2(-Mathf.Abs(combo.transform.localScale.x), combo.transform.localScale.y);
                    }
                }
                else
                {
                    if (lookingRight)
                    {
                        combo.transform.position = new Vector2(
                            transform.position.x + (GetComponent<CapsuleCollider2D>().size.x),
                            transform.position.y);
                        if (lookingRight)
                        {
                            combo.transform.localScale = new Vector2(Mathf.Abs(combo.transform.localScale.x), combo.transform.localScale.y);
                        }
                    }
                    else
                    {
                        combo.transform.position = new Vector2(
                            transform.position.x - (GetComponent<CapsuleCollider2D>().size.x),
                            transform.position.y);
                        if (lookingRight)
                        {
                            combo.transform.localScale = new Vector2(Mathf.Abs(combo.transform.localScale.x), combo.transform.localScale.y);
                        }
                    }
                }
                combo.transform.parent = transform;

                isAttacking = true;
                currentFrame = 0;
                frameStartTime = Time.time;
                if (Input.GetAxis("Look Up") > 0.1)
                {
                    GetComponent<SpriteRenderer>().sprite = attackUpSeiko[currentFrame];
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = attack1Seiko[currentFrame];
                }
            }

            //Attack 2
            if (Input.GetButtonDown("Fire2") && !isAttacking)
            {
                GameObject combo = Instantiate(attacks[1]) as GameObject;
                if (Input.GetAxis("Look Up") > 0.1)
                {
                    combo.transform.position = new Vector2(
                        transform.position.x,
                        transform.position.y + transform.lossyScale.y);
                    combo.GetComponent<Attack1Script>().GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, combo.GetComponent<Attack1Script>().speed);
                }
                else
                {
                    if (lookingRight)
                    {
                        combo.transform.position = new Vector2(
                            transform.position.x + transform.lossyScale.x,
                            transform.position.y);
                        combo.GetComponent<Attack1Script>().GetComponent<Rigidbody2D>().velocity = new Vector2(
                            combo.GetComponent<Attack1Script>().speed, 0.0f);
                    }
                    else
                    {
                        combo.transform.position = new Vector2(
                            transform.position.x,
                            transform.position.y);
                        combo.GetComponent<Attack1Script>().GetComponent<Rigidbody2D>().velocity = new Vector2(
                            -combo.GetComponent<Attack1Script>().speed, 0.0f);
                    }
                }

                isAttacking = true;
                currentFrame = 0;
                frameStartTime = Time.time;
                if (Input.GetAxis("Look Up") > 0.1)
                {
                    GetComponent<SpriteRenderer>().sprite = attackUpSeiko[currentFrame];
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = attack1Seiko[currentFrame];
                }
            }

            //Apply movement
            if(move.x > 0 && GetComponent<Rigidbody2D>().velocity.x < move.x)
            {
                GetComponent<Rigidbody2D>().velocity += new Vector2(move.x, 0.0f);
            }
            else if(move.x < 0 && GetComponent<Rigidbody2D>().velocity.x > move.x)
            {
                GetComponent<Rigidbody2D>().velocity += new Vector2(move.x, 0.0f);
            }
            if (move.y > 0 && GetComponent<Rigidbody2D>().velocity.y < move.y)
            {
                GetComponent<Rigidbody2D>().velocity += new Vector2(0.0f, move.y);
            }
            else if (move.y < 0 && GetComponent<Rigidbody2D>().velocity.y > maxFallSpeed)
            {
                GetComponent<Rigidbody2D>().velocity += new Vector2(0.0f, move.y);
            }
            if(move.x < maxSpeed)
            {
                if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) < rateOfStopping)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, GetComponent<Rigidbody2D>().velocity.y);
                }
                else if(GetComponent<Rigidbody2D>().velocity.x <= 0)
                {
                    GetComponent<Rigidbody2D>().velocity += new Vector2(rateOfStopping, 0.0f);
                }
                else
                {
                    GetComponent<Rigidbody2D>().velocity -= new Vector2(rateOfStopping, 0.0f);
                }
            }
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        }
        /*
        if (isInvincible)
        {
            if (currentSpeed < maxSpeed)
            {
                currentSpeed = maxSpeed;
            }
        }*/
    }

    public void takeDamage(int damage, Vector2 knockback)
    {
        if (!isInvincible)
        {
            if (shield > 0)
            {
                shield -= damage;
                if (shield < 0)
                {
                    shield = 0;
                }
                UIDungeon.GrabShield(shield);
            }
            if(shield < 0) { 
                if (health > 0)
                {
                    health -= 1;
                    UIDungeon.GrabHealth(health);
                    if (health == 0)
                    {
                        SceneManager.LoadScene("DemoMenu");
                    }
                }
            }
            isInvincible = true;
            timeOfHit = Time.time;
            GetComponent<Rigidbody2D>().velocity = new Vector2(knockback.x, knockback.y);
        }
    }

    public Vector2 GetPosition()
    {
        if (Input.GetAxis("Look Up") < 0.1)
        {
            if (Input.GetAxis("Look Down") < 0.1)
            {
                return transform.position;
            }
            else
            {
                return new Vector2(transform.position.x, transform.position.y - lookDownDistance);
            }
        }
        else
        {
            return new Vector2(transform.position.x, transform.position.y + lookUpDistance);
        }
    }
}
