using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public float maxSpeed = 7;
    public float takeOffSpeed = 7;
    public float maxJumpHeight = 10;
    public float gravity = 0.01f;
    public bool canControl = true;

    private bool isGrounded = false;
    private bool isJumping = false;
    private int groundingCollisions = 0;
    private Vector2 move;
    private float distToGround;
    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        distToGround = player.GetComponent<BoxCollider2D>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        
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

            //Vertical Movement
            if (isGrounded)
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
                move.y -= gravity;
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
        if(collision.gameObject.tag == "Solid")
        {
            groundingCollisions++;
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Solid")
        {
            groundingCollisions--;
            if(groundingCollisions == 0)
            {
                isGrounded = false;
            }
        }
    }

}
