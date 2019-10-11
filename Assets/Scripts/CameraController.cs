using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private float XBounds = 3f;
    private float YMin = 3f;
    private float YMax = 0.5f;
    private float XDivisor = 0.5f;
    private float YDivisor = 1.5f;
    private float radius = 1;
    private float downRadius = 4;
    private float YOffset;
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
        //Player moves too far to the sides
        if(Mathf.Round(player.transform.position.x) < Mathf.Round(transform.position.x - XBounds))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(
                (player.transform.position.x - transform.position.x) / XDivisor, 
                GetComponent<Rigidbody2D>().velocity.y);
        }
        else if(Mathf.Round(player.transform.position.x) > Mathf.Round(transform.position.x + XBounds))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(
                -(transform.position.x - player.transform.position.x) / XDivisor, 
                GetComponent<Rigidbody2D>().velocity.y);
        }
        else if(Mathf.Abs(player.transform.position.x - transform.position.x) < radius)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, GetComponent<Rigidbody2D>().velocity.y);
        }

        //Player moves too far vertically
        if(Mathf.Round(player.transform.position.y) < Mathf.Round(transform.position.y - YMin))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(
                GetComponent<Rigidbody2D>().velocity.x, 
                -Mathf.Pow(((transform.position.y - player.transform.position.y) / (YDivisor * 1.7f)), 
                2));
        }
        else if(Mathf.Round(player.transform.position.y) > Mathf.Round(transform.position.y + YMax))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(
                GetComponent<Rigidbody2D>().velocity.x, 
                Mathf.Pow(((player.transform.position.y - transform.position.y) / YDivisor), 
                2));
        }
        else if(player.transform.position.y - transform.position.y < radius && player.transform.position.y > transform.position.y)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0.0f);
            if(YOffset == float.NaN)
            {
                YOffset = transform.position.y - player.transform.position.y;
            }
        }
        else if(transform.position.y - player.transform.position.y < downRadius)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0.0f);
            if (YOffset == float.NaN)
            {
                YOffset = transform.position.y - player.transform.position.y;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 10)
        {
            print("HIt camera wall");
        }
    }
}
