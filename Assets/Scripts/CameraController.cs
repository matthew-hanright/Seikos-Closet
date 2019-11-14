using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private float XBounds = 3f;
    private float YMin = 3f;
    private float YMax = 10f;
    private float XDivisor = 0.5f;
    private float YDownDivisor = 1.5f;
    public float YUpDivisor = 1.0f;
    private float radius = 2;
    private float downRadius = 3;
    private float YOffset;

    public float xSpeedMultiplier = 1;
    public float ySpeedMultiplier = 1;

    public bool followPlayer = true;
    public bool needToChangeLayer = false;
    private bool tooFarHorizontal = false;
    private bool tooFarVertical = false;

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
        if (followPlayer)
        {
            //Player moves too far to the sides
            if (Mathf.Round(player.transform.position.x) < Mathf.Round(transform.position.x - XBounds))
            {
                tooFarHorizontal = true;
                GetComponent<Rigidbody2D>().velocity = new Vector2(
                    ((player.transform.position.x - transform.position.x) / XDivisor) * xSpeedMultiplier,
                    GetComponent<Rigidbody2D>().velocity.y);
            }
            else if (Mathf.Round(player.transform.position.x) > Mathf.Round(transform.position.x + XBounds))
            {
                tooFarHorizontal = true;
                GetComponent<Rigidbody2D>().velocity = new Vector2(
                    (-(transform.position.x - player.transform.position.x) / XDivisor) * xSpeedMultiplier,
                    GetComponent<Rigidbody2D>().velocity.y);
            }
            else if (Mathf.Abs(player.transform.position.x - transform.position.x) < radius)
            { //If the player is within the bounds, stop moving along x
                tooFarHorizontal = false;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, GetComponent<Rigidbody2D>().velocity.y);
            }

            //Player moves too far vertically
            if (Mathf.Round(player.transform.position.y) < Mathf.Round(transform.position.y - YMin))
            {
                tooFarVertical = true;
                GetComponent<Rigidbody2D>().velocity = new Vector2(
                    GetComponent<Rigidbody2D>().velocity.x,
                    (-Mathf.Pow(((transform.position.y - player.transform.position.y) / (YDownDivisor * 1.7f)),
                    2)) * ySpeedMultiplier);
            }
            else if (Mathf.Round(player.transform.position.y) > Mathf.Round(transform.position.y + YMax))
            {
                tooFarVertical = true;
                GetComponent<Rigidbody2D>().velocity = new Vector2(
                    GetComponent<Rigidbody2D>().velocity.x,
                    (Mathf.Pow(((player.transform.position.y - transform.position.y) / YUpDivisor),
                    2)) * ySpeedMultiplier);
            }
            else if (player.transform.position.y - transform.position.y < radius && player.transform.position.y > transform.position.y)
            { //If the player is within the bounds, stop moving along y
                tooFarVertical = false;
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0.0f);
                if (YOffset == float.NaN)
                {
                    YOffset = transform.position.y - player.transform.position.y;
                }
            }
            else if (transform.position.y - player.transform.position.y < downRadius)
            {
                tooFarVertical = false;
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0.0f);
                if (YOffset == float.NaN)
                {
                    YOffset = transform.position.y - player.transform.position.y;
                }
            }

            if(!tooFarHorizontal && !tooFarVertical && needToChangeLayer)
            {
                gameObject.layer = 10;
                needToChangeLayer = false;
            }
        }
    }
}
