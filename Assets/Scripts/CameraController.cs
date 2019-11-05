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
    private float downRadius = 3;
    private float YOffset;

    public float xSpeedMultiplier = 1;
    public float ySpeedMultiplier = 1;

    public bool followPlayer = true;
    public bool needToChangeLayer = false;
    private bool tooFarHorizontal = false;
    private bool tooFarVertical = false;

    public Vector3 FINAL_POSITION;
    public Vector3 FINAL_SCALE;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = FINAL_POSITION;
        transform.localScale = FINAL_SCALE;
        GetComponent<Camera>().orthographicSize = 23.52861f;
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
                    (-Mathf.Pow(((transform.position.y - player.transform.position.y) / (YDivisor * 1.7f)),
                    2)) * ySpeedMultiplier);
            }
            else if (Mathf.Round(player.transform.position.y) > Mathf.Round(transform.position.y + YMax))
            {
                tooFarVertical = true;
                GetComponent<Rigidbody2D>().velocity = new Vector2(
                    GetComponent<Rigidbody2D>().velocity.x,
                    (Mathf.Pow(((player.transform.position.y - transform.position.y) / YDivisor),
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
