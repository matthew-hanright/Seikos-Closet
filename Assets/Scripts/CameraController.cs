using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private bool playerContained = true;

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

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerContained = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("Player left");
            playerContained = false;
            while (!playerContained)
            {
                if (player.transform.position.x < transform.position.x) //Out on left
                {
                    if (!Physics2D.OverlapPoint(new Vector2(transform.position.x - 1, transform.position.y)))
                    {
                        transform.position = new Vector2(transform.position.x - 1, transform.position.y);
                    }
                }
                else if (player.transform.position.x > transform.position.x) //Out on right
                {
                    if (!Physics2D.OverlapPoint(new Vector2(transform.position.x + 1, transform.position.y)))
                    {
                        transform.position = new Vector2(transform.position.x + 1, transform.position.y);
                    }
                }
                if (player.transform.position.y < transform.position.y) //Out down
                {
                    if (!Physics2D.OverlapPoint(new Vector2(transform.position.x, transform.position.y - 1)))
                    {
                        transform.position = new Vector2(transform.position.x, transform.position.y - 1);
                    }
                }
                else if (player.transform.position.y > transform.position.y)
                {
                    if (!Physics2D.OverlapPoint(new Vector2(transform.position.x, transform.position.y + 1)))
                    {
                        transform.position = new Vector2(transform.position.x, transform.position.y + 1);
                    }
                }
            }
        }
    }

}
