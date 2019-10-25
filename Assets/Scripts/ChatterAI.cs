using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatterAI : ParentEnemy
{
    public bool movingLeft = true;
    public float moveSpeed = 2.0f;
    public float runSpeed = 4.0f;
    public bool seesPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(isGrounded)
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
    }

    public void turn(bool direction)
    {
        if(direction)
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
