using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    [Header ("Must be the left point")]
    public Vector2 startPoint;
    [Header("Must be the right point")]
    public Vector2 endPoint;
    public bool shouldMove = true;
    public float magicForceNumber = 23;  //Magic constant to make the player move the right speed when on the platform
    public float speed; //Number of times to subdivide the distance, higher is slower
    public bool currentGoalPoint; //True for moving to start, false for moving to end
    private float intervalX, intervalY;

    // Start is called before the first frame update
    void Start()
    {
        if(startPoint.x == endPoint.x)
        {
            GetComponent<SurfaceEffector2D>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (shouldMove)
        {
            if (currentGoalPoint)
            {
                GetComponent<SurfaceEffector2D>().speed = -Mathf.Abs(GetComponent<SurfaceEffector2D>().speed);
                float xDiscrepancy = Mathf.Abs(transform.position.x - startPoint.x);
                float yDiscrepancy = Mathf.Abs(transform.position.y - startPoint.y);
                if (xDiscrepancy < 1.0f && yDiscrepancy < 1.0f)
                {
                    currentGoalPoint = false;
                    intervalX = (endPoint.x - startPoint.x) / speed;
                    intervalY = (endPoint.y - startPoint.y) / speed;
                }
                else
                {
                    intervalX = (startPoint.x - endPoint.x) / speed;
                    intervalY = (startPoint.y - endPoint.y) / speed;
                }
            }
            else
            {
                GetComponent<SurfaceEffector2D>().speed = Mathf.Abs(GetComponent<SurfaceEffector2D>().speed);
                float xDiscrepancy = Mathf.Abs(transform.position.x - endPoint.x);
                float yDiscrepancy = Mathf.Abs(transform.position.y - endPoint.y);
                if (xDiscrepancy < 1.0f && yDiscrepancy < 1.0f)
                {
                    currentGoalPoint = true;
                    intervalX = (startPoint.x - endPoint.x) / speed;
                    intervalY = (startPoint.y - endPoint.y) / speed;
                }
                else
                {
                    intervalX = (endPoint.x - startPoint.x) / speed;
                    intervalY = (endPoint.y - startPoint.y) / speed;
                }
            }
            GetComponent<Rigidbody2D>().velocity = new Vector2(intervalX, intervalY);
        }
    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (shouldMove && collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(
                collision.gameObject.GetComponent<Rigidbody2D>().velocity.x - 
                GetComponent<Rigidbody2D>().velocity.x,
                0.0f);
            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(intervalX * magicForceNumber, 0.0f));
        }
    }*/

}
