using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatterAI : ParentEnemy
{
    public bool movingLeft = true;
    public float moveSpeed = 2.0f;
    public float runSpeed = 4.0f;
    public bool seesPlayer = false;
    private bool stopMoving = false;
    private bool needToTurn = false;
    // Start is called before the first frame update
    void Start()
    {
        damage = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(needToTurn && !seesPlayer)
        {
            turn(movingLeft);
            needToTurn = false;
        }
    }

    private void FixedUpdate()
    {
        if(isGrounded && !seesPlayer && !stopMoving && !needToTurn)
        {
            if(movingLeft)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
        }
        else if(isGrounded && seesPlayer && !stopMoving && !needToTurn)
        {
            if (movingLeft)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-runSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(runSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
        }
        else if(stopMoving || needToTurn)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    public void turn(bool direction)
    {
        if (seesPlayer)
        {
            needToTurn = true;
        }
        else
        {
            if (direction)
            {
                movingLeft = false;
                transform.localScale = new Vector3(
                    -Mathf.Abs(transform.localScale.x),
                    transform.localScale.y,
                    transform.localScale.z);
            }
            else
            {
                movingLeft = true;
                transform.localScale = new Vector3(
                    Mathf.Abs(transform.localScale.x),
                    transform.localScale.y,
                    transform.localScale.z);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            stopMoving = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            stopMoving = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().takeDamage(damage);
        }
    }
}
