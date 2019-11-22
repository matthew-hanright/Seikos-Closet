using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector2 TrackingPoint;

    private float XBounds = 0f;
    private float YMin = 0f;
    public float YMax = 0f;
    private float XDivisor = 0.5f;
    private float YDownDivisor = 1.5f;
    private float YUpDivisor = 5f;
    private float radius = 2;
    private float downRadius = 0;

    public float xSpeedMultiplier = 1;
    public float ySpeedMultiplier = 1;

    public bool followPlayer = true;
    public bool needToChangeLayer = false;

    public float maxYSpeed = 20f;

    private void LateUpdate()
    {
        TrackingPoint = player.GetComponent<PlayerController>().GetPosition();
        if (followPlayer)
        {
            //Player moves too far to the sides
            if (Mathf.Round(TrackingPoint.x) < Mathf.Round(transform.position.x - XBounds))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(
                    (-(transform.position.x - TrackingPoint.x) / XDivisor) * xSpeedMultiplier,
                    GetComponent<Rigidbody2D>().velocity.y);
            }
            else if (Mathf.Round(TrackingPoint.x) > Mathf.Round(transform.position.x + XBounds))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(
                    (-(transform.position.x - TrackingPoint.x) / XDivisor) * xSpeedMultiplier,
                    GetComponent<Rigidbody2D>().velocity.y);
            }
            else if (Mathf.Abs(TrackingPoint.x - transform.position.x) < radius)
            { //If the player is within the bounds, stop moving along x
                GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, GetComponent<Rigidbody2D>().velocity.y);
            }

            //Player moves too far vertically
            if (Mathf.Round(TrackingPoint.y) < Mathf.Round(transform.position.y - YMin))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(
                    GetComponent<Rigidbody2D>().velocity.x,
                    (-Mathf.Pow(((transform.position.y - TrackingPoint.y) / (YDownDivisor * 1.7f)),
                    2)) * ySpeedMultiplier);

                if(GetComponent<Rigidbody2D>().velocity.y > maxYSpeed)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(
                        GetComponent<Rigidbody2D>().velocity.x, maxYSpeed);
                }
                else if(GetComponent<Rigidbody2D>().velocity.y < -maxYSpeed)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(
                        GetComponent<Rigidbody2D>().velocity.x, -maxYSpeed);
                }
            }
            else if (Mathf.Round(TrackingPoint.y) > Mathf.Round(transform.position.y + YMax))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(
                    GetComponent<Rigidbody2D>().velocity.x,
                    (Mathf.Pow(((TrackingPoint.y - transform.position.y) / YUpDivisor),
                    2)) * ySpeedMultiplier);

                if (GetComponent<Rigidbody2D>().velocity.y > maxYSpeed)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(
                        GetComponent<Rigidbody2D>().velocity.x, maxYSpeed);
                }
                else if (GetComponent<Rigidbody2D>().velocity.y < -maxYSpeed)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(
                        GetComponent<Rigidbody2D>().velocity.x, -maxYSpeed);
                }
            }
            else if (TrackingPoint.y - transform.position.y < radius && TrackingPoint.y > transform.position.y)
            { //If the player is within the bounds, stop moving along y
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0.0f);
            }
            else if (transform.position.y - TrackingPoint.y < downRadius)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0.0f);
            }
        }
    }
}
