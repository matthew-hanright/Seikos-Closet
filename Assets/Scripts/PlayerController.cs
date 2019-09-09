using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public int maxSpeed = 7;
    public int takeOffSpeed = 7;

    private bool isGrounded;
    private Vector2 move;
    private float distToGround;
    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        distToGround = player.GetComponent<BoxCollider2D>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        //Interactions

        //Movement
        move.x = Input.GetAxis("Horizontal");
        if(move.x > maxSpeed)
        {
            move.x = maxSpeed;
        }
        if (move.x < 0)
        {
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
                player.GetComponentInChildren<BoxCollider2D>().offset = new Vector2(
                    player.GetComponentInChildren<BoxCollider2D>().offset.x * -1,
                    player.GetComponentInChildren<BoxCollider2D>().offset.y);
            }
        }
        else if (move.x > 0)
        {
            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
                player.GetComponentInChildren<BoxCollider2D>().offset = new Vector2(
                    player.GetComponentInChildren<BoxCollider2D>().offset.x * -1,
                    player.GetComponentInChildren<BoxCollider2D>().offset.y);
            }
        }
        player.transform.Translate(move);
    }

}
