using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private bool isInvincible = false;
    private bool isGrounded = false;
    private bool isJumping = false;
    private int groundingCollisions = 0;
    private Vector2 move;
    private float distToGround;
    private float timeOfHit;

    public Sprite neutral;
    public Sprite walking;

    private int framesPerSecond = 10;
	
    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        distToGround = player.GetComponent<BoxCollider2D>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= timeOfHit + invincibilityLength)
        {
            isInvincible = false;
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
                GetComponent<SpriteRenderer>().sprite = walking;
            }
            if (move.x < 0)
            {
                if (transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
                    //Swap which side the interact box is on when the player changes which direction
                    //they are facing
                    player.GetComponentInChildren<BoxCollider2D>().offset = new Vector2(
                        player.GetComponentInChildren<BoxCollider2D>().offset.x * -1,
                        player.GetComponentInChildren<BoxCollider2D>().offset.y);
                }
            }
            else if (move.x > 0)
            {
                if (transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
                    player.GetComponentInChildren<BoxCollider2D>().offset = new Vector2(
                        player.GetComponentInChildren<BoxCollider2D>().offset.x * -1,
                        player.GetComponentInChildren<BoxCollider2D>().offset.y);
                }
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = neutral;
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
            player.GetComponent<Rigidbody2D>().velocity = move;
        }
        else
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Solid" && collision.gameObject.transform.position.y < transform.position.y)
        {
            groundingCollisions++;
            isGrounded = true;
        }
        else if (collision.gameObject.tag == "Solid" && collision.gameObject.transform.position.y > transform.position.y)
        {
            isJumping = false;
            move.y = 0;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, 0.0f);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Solid" && groundingCollisions > 0)
        {
            groundingCollisions--;
            if(groundingCollisions == 0)
            {
                isGrounded = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Solid")
        {
            if(collision.gameObject.transform.position.y > transform.position.y)
            {
                isJumping = false;
                move.y -= gravity;
            }
            else if(collision.gameObject.transform.position.y < transform.position.y)
            {
                move.y += gravity;
            }
        }
    }

    public void takeDamage(int damage)
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
            }
            else
            {
                if (health > 0)
                {
                    health -= 1;
                    if (health == 0)
                    {
                        Time.timeScale = 0;
                    }
                }
            }
            print("Vulnerable, health: " + health + "; shield: " + shield);
            isInvincible = true;
            timeOfHit = Time.time;
        }
        else
        {
            print("Invulnerable, health: " + health + "; shield: " + shield);
        }
    }
}
