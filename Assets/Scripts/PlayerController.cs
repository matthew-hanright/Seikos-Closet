using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static GameObject attack1;
    public static GameObject attack2;
    public static GameObject attack3;
    public static int MAX_HEALTH = 2;
    public static int MAX_SHIELD = 100;

    public GameObject player;
    public GameObject[] attacks = { attack1, attack2, attack3 };
    public float maxSpeed = 7;
    public float takeOffSpeed = 7;
    public float maxJumpHeight = 10;
    public float gravity = 0.01f;
    public bool canControl = true;
    public float invincibilityLength = 0.2f;
	
	public int health = MAX_HEALTH;
	public int shield = MAX_SHIELD;

    private bool isWalking = false;
    private bool isInvincible = false;
    public bool isGrounded = false;
    private bool isJumping = false;
    private Vector2 move;
    private float timeOfHit;

    public Sprite neutral;
    public Sprite[] walking;
    private int walkingSubSprite = 0;
    private float frameRate = 0.12f;
    private float frameStartTime;
	
    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        frameStartTime = Time.time;
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
    }

    private void OnGUI()
    {
        if (isWalking && canControl)
        {
            if (Time.time >= frameStartTime + frameRate)
            {
                if (walkingSubSprite < walking.Length - 1)
                {
                    walkingSubSprite += 1;
                }
                else
                {
                    walkingSubSprite = 0;
                }
                frameStartTime = Time.time;
                GetComponent<SpriteRenderer>().sprite = walking[walkingSubSprite];
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
                GetComponent<SpriteRenderer>().sprite = walking[walkingSubSprite];
                isWalking = true;
            }
            if (move.x < 0)
            {
                if (transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
                    //Swap which side the interact box is on when the player changes which direction
                    //they are facing
                    GetComponentInChildren<BoxCollider2D>().offset = new Vector2(
                        GetComponentInChildren<BoxCollider2D>().offset.x * -1,
                        GetComponentInChildren<BoxCollider2D>().offset.y);
                }
            }
            else if (move.x > 0)
            {
                if (transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
                    //Flip the interact box as well
                    GetComponentInChildren<BoxCollider2D>().offset = new Vector2(
                        GetComponentInChildren<BoxCollider2D>().offset.x * -1,
                        GetComponentInChildren<BoxCollider2D>().offset.y);
                }
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = neutral;
                isWalking = false;
            }

            //Vertical Movement
            if (isGrounded && move.y < 1.0f)
            {
                if (Input.GetAxis("Vertical") > 0)
                {
                    isJumping = true;
                    move.y += takeOffSpeed;
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
			
			if(Input.GetButtonDown("Fire1")){
				GameObject combo = Instantiate(attacks[0]) as GameObject;
                combo.transform.position = new Vector2(transform.position.x + 0.16f, transform.position.y);

			}
			
            //Apply movement
            GetComponent<Rigidbody2D>().velocity = move;
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Solid" && 
            collision.gameObject.layer != 11 &&
            collision.gameObject.transform.position.y > transform.position.y)
        {
            isJumping = false;
            move.y = 0;
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0.0f);
        }
    }

    public void takeDamage(int damage)
    {
        if (!isInvincible)
        {
            print("Shield: " + shield + "; health: " + health);
            if (shield > 0)
            {
                shield -= damage;
                if (shield < 0)
                {
                    shield = 0;
                }
            }
            else
            {
                if (health > 0)
                {
                    health -= 1;
                    if (health == 0)
                    {
                        SceneManager.LoadScene("DemoMenu");
                    }
                }
            }
            isInvincible = true;
            timeOfHit = Time.time;
        }
    }
}
