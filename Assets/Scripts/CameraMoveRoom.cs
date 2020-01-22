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
