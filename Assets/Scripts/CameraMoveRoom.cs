using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveRoom : MonoBehaviour
{
    public GameObject camera;
    //public GameObject rightDownPoint, transferPoint, leftUpPoint;
    //private int transitionMultiplier = 5;
    [Header("True means the transition is left/right, false up/down")]
    public bool LRUD; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Previous attempt, only kept around for possible future use
    /*
    //When the player enters the camera is free to move
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            camera.layer = 12;
            camera.GetComponent<CameraController>().xSpeedMultiplier = 1f;
            camera.GetComponent<CameraController>().ySpeedMultiplier = 1f;
            camera.GetComponent<CameraController>().followPlayer = false;
            camera.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }

    //The player is now in a room, so the camera must collide again
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            camera.GetComponent<CameraController>().needToChangeLayer = true;
            camera.GetComponent<CameraController>().xSpeedMultiplier = 1f;
            camera.GetComponent<CameraController>().ySpeedMultiplier = 1f;
            camera.GetComponent<CameraController>().followPlayer = true;
            if(LRUD)
            {
                if(collision.gameObject.transform.position.x < transform.position.x)
                {
                    camera.GetComponent<Rigidbody2D>().velocity = new Vector2(
                        (leftUpPoint.transform.position.x - transferPoint.transform.position.x) * transitionMultiplier,
                        camera.GetComponent<Rigidbody2D>().velocity.y);
                }
                else if(collision.gameObject.transform.position.x > transform.position.x)
                {
                    camera.GetComponent<Rigidbody2D>().velocity = new Vector2(
                        (rightDownPoint.transform.position.x - transferPoint.transform.position.x) * transitionMultiplier,
                        camera.GetComponent<Rigidbody2D>().velocity.y);
                }
            }
            else
            {
                if(collision.gameObject.transform.position.y < transform.position.y)
                {
                    print("out down");
                    camera.GetComponent<Rigidbody2D>().velocity = new Vector2(
                        camera.GetComponent<Rigidbody2D>().velocity.x,
                        (rightDownPoint.transform.position.y - transform.position.y) * transitionMultiplier);
                    camera.transform.position += new Vector3(-2.0f, 0.0f, 0.0f);
                }
                else if(collision.gameObject.transform.position.y > transform.position.y)
                {
                    print("Out up");
                    camera.GetComponent<Rigidbody2D>().velocity = new Vector2(
                        camera.GetComponent<Rigidbody2D>().velocity.x,
                        (leftUpPoint.transform.position.y - transform.position.y) * transitionMultiplier);
                    camera.transform.position += new Vector3(2.0f, 0.0f, 0.0f);
                }
            }
        }
    }*/

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (LRUD)
            {
                if (collision.transform.position.x < transform.position.x &&
                    camera.transform.position.x > transform.position.x)
                {
                    camera.transform.position = new Vector3(
                        camera.transform.position.x - camera.GetComponent<BoxCollider2D>().bounds.extents.x,
                        camera.transform.position.y,
                        camera.transform.position.z);
                }
                else if (collision.transform.position.x > transform.position.x &&
                    camera.transform.position.x < transform.position.x)
                {
                    camera.transform.position = new Vector3(
                        camera.transform.position.x + camera.GetComponent<BoxCollider2D>().bounds.extents.x,
                        camera.transform.position.y,
                        camera.transform.position.z);
                }
            }
            else
            {
                if(collision.transform.position.y < transform.position.y && 
                    camera.transform.position.y > transform.position.y)
                {
                    camera.transform.position = new Vector3(
                    camera.transform.position.x,
                    transform.position.y - camera.GetComponent<BoxCollider2D>().bounds.extents.y,
                    camera.transform.position.z);
                }
                else if(collision.transform.position.y > transform.position.y &&
                    camera.transform.position.y < transform.position.y)
                {
                    camera.transform.position = new Vector3(
                    camera.transform.position.x,
                    transform.position.y + camera.GetComponent<BoxCollider2D>().bounds.extents.y,
                    camera.transform.position.z);
                }
            }
        }
    }
}
